using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Controllers.Team;
using ZombieDiorama.Character.Behaviours;
using ZombieDiorama.Character.Behaviours.Soldier;
using ZombieDiorama.Character.Behaviours.Custom;
using ZombieDiorama.Character.Behaviours.Decorators;
using ZombieDiorama.Utilities;

namespace ZombieDiorama.Character.AIs
{
    public class Soldier : MonoBehaviour, AIBase
    {
        [Title("Geral Info")]
        public float Speed = 6;

        [Title("Regroup")]
        public float distanceToRegroup = 1;

        [Title("Team")]
        public float distanceToLeader = 1;

        [Title("Geral Controllers")]
        public SquadMember squadMember;
        public TargetController targetController;
        public NavMeshController navMeshController;
        protected BehaviourTree behaviourTree;

        [Title("Fight")]
        public GameObject muzzle;
        public GameObject bullet;
        public float shootCooldown = 0.5f;
        public float lookAtZombieDamping = 15f;

        [Title("Combat")]
        public string target = "Zombie";
        public float distanceToTarget = 1;
        public Vector2 rangeToSeeTarget = new Vector2(5, 20);

        [Title("AI Attack Events")]
        public EventCaller AttackEventCaller;

        [Title("Gizmos")]
        public bool ShowGizmos = true;
        public Color MaxRangeToSee = Color.blue;
        public Color MinRangeToSee = Color.cyan;

        #region  SETUP
        protected virtual void Awake()
        {
            targetController = GetComponent<TargetController>();
            navMeshController = GetComponent<NavMeshController>();
            squadMember = GetComponent<SquadMember>();
        }

        protected virtual void Start()
        {
            SetBehaviour();
        }

        public virtual void SetBehaviour()
        {
            if (!behaviourTree)
            {
                behaviourTree = gameObject.AddComponent<BehaviourTree>();
            }

            if (!navMeshController)
            {
                navMeshController = gameObject.AddComponent<NavMeshController>();
            }

            if (!targetController)
            {
                targetController = gameObject.AddComponent<TargetController>();
            }

            BTSelector root = new BTSelector();
            // REGROUP
            BTNode branchRegroup = GetBranchRegroup();
            // FIGHT
            BTNode fight = GetBranchFight();
            // FOLLOW LEADER
            BTNode branchTeam = GetBranchTeam();

            root.SetNode(branchRegroup);
            root.SetNode(fight);
            root.SetNode(branchTeam);

            behaviourTree.Build(root);
        }

        public virtual void RestartBehaviour()
        {
            SetBehaviour();
            if (behaviourTree)
            {
                behaviourTree.enabled = true;
                behaviourTree.Initialize();
            }
        }

        public virtual void StopBehaviour()
        {
            if (behaviourTree)
            {
                behaviourTree.Stop();
                behaviourTree.enabled = false;
                behaviourTree.StopAllCoroutines();
            }
        }
        #endregion

        #region FIGHT
        public virtual BTNode GetBranchFight()
        {
            BTParallelSelector parallel = new BTParallelSelector();
            parallel.SetNode(new BTMoveByNavMesh(navMeshController, targetController, Speed, rangeToSeeTarget.x));
            parallel.SetNode(new BTCloseToTarget(targetController, rangeToSeeTarget.x, rangeToSeeTarget.y));
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(targetController, distanceToRegroup);
            parallel.SetNode(calledToRegroup);

            BTInverter inverter = new BTInverter();
            BTSeeZombie seeZombie = new BTSeeZombie(targetController, rangeToSeeTarget.y);
            inverter.SetNode(seeZombie);
            parallel.SetNode(inverter);

            BTSequence sequence_1 = new BTSequence();
            sequence_1.SetNode(parallel);
            sequence_1.SetNode(new BTSoldierAttack(targetController, shootCooldown, bullet, muzzle, lookAtZombieDamping, target, AttackEventCaller));

            BTSequence sequence = new BTSequence();
            sequence.SetNode(new BTSeeZombie(targetController, rangeToSeeTarget.y));
            sequence.SetNode(sequence_1);

            BTSelector selector = new BTSelector();
            selector.SetNode(sequence);

            return selector;
        }
        #endregion

        #region REAGROUP
        public BTNode GetBranchRegroup()
        {
            BTSequence sequence_regroup = new BTSequence();

            #region Cheking
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(targetController, distanceToRegroup);
            BTUpdateRegroup updateRegroup = new BTUpdateRegroup(targetController);
            #endregion

            #region Moving
            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTNextToTarget nextToTarget = new BTNextToTarget(targetController, distanceToRegroup);
            BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(navMeshController, targetController, Speed, distanceToRegroup);

            parallelSelector_1.SetNode(nextToTarget);
            parallelSelector_1.SetNode(moveToSet);
            #endregion

            #region  Apply Node
            sequence_regroup.SetNode(calledToRegroup);
            sequence_regroup.SetNode(updateRegroup);
            sequence_regroup.SetNode(parallelSelector_1);
            #endregion

            return sequence_regroup;
        }
        #endregion

        #region TEAM
        public BTNode GetBranchTeam()
        {
            BTSelector sequence_team = new BTSelector();

            #region Leader Branch
            BTNode teamLeaderBranch = GetBranchTeamLeader();
            #endregion

            #region Member Branch
            BTNode teamMemberBranch = GetBranchTeamMember();
            #endregion

            #region Without Leader
            BTNode teamWithoutLeader = GetBranchWithoutLeader();
            #endregion

            #region  Apply Node
            sequence_team.SetNode(teamLeaderBranch);
            sequence_team.SetNode(teamMemberBranch);
            sequence_team.SetNode(teamWithoutLeader);
            #endregion

            return sequence_team;
        }
        #endregion

        #region TEAM LEADER
        public BTNode GetBranchTeamLeader()
        {
            BTSequence sequence = new BTSequence();

            BTIsLeader isLeader = new BTIsLeader(squadMember);
            BTThereIs thereIs = new BTThereIs(targetController, target);

            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTSee see = new BTSee(targetController, target, rangeToSeeTarget.y);
            BTMoveByNavMesh moveTo = new BTMoveByNavMesh(navMeshController, targetController, Speed, rangeToSeeTarget.x);

            parallelSelector_1.SetNode(see);
            parallelSelector_1.SetNode(moveTo);

            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(squadMember);
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(targetController, distanceToRegroup);
            parallelSelector_1.SetNode(thereIsObjectToSet);
            parallelSelector_1.SetNode(calledToRegroup);

            sequence.SetNode(isLeader);
            sequence.SetNode(thereIs);
            sequence.SetNode(parallelSelector_1);

            return sequence;

        }
        #endregion

        #region  TEAM MEMBER
        public BTNode GetBranchTeamMember()
        {
            BTSequence sequence_2 = new BTSequence();

            BTHasLeader hasLeader = new BTHasLeader(squadMember);
            BTUpdateLeader updateLeader = new BTUpdateLeader(squadMember, targetController);
            sequence_2.SetNode(hasLeader);
            sequence_2.SetNode(updateLeader);

            BTParallelSelector parallelSelector_2 = new BTParallelSelector();
            sequence_2.SetNode(parallelSelector_2);

            BTSee see_2 = new BTSee(targetController, target, distanceToTarget, true);
            BTMoveByNavMesh moveLeader = new BTMoveByNavMesh(navMeshController, targetController, Speed, distanceToLeader);

            parallelSelector_2.SetNode(see_2);
            parallelSelector_2.SetNode(moveLeader);

            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(squadMember);
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(targetController, distanceToRegroup);
            parallelSelector_2.SetNode(thereIsObjectToSet);
            parallelSelector_2.SetNode(calledToRegroup);

            sequence_2.SetNode(parallelSelector_2);

            return sequence_2;
        }
        #endregion

        #region WITHOUT LEADER
        public BTNode GetBranchWithoutLeader()
        {
            BTSequence sequence = new BTSequence();
            BTThereIs thereIs = new BTThereIs(targetController, target);

            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTSee see = new BTSee(targetController, target, rangeToSeeTarget.y);
            BTMoveByNavMesh moveTo = new BTMoveByNavMesh(navMeshController, targetController, Speed, rangeToSeeTarget.x);

            parallelSelector_1.SetNode(see);
            parallelSelector_1.SetNode(moveTo);

            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(squadMember);
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(targetController, distanceToRegroup);
            parallelSelector_1.SetNode(thereIsObjectToSet);
            parallelSelector_1.SetNode(calledToRegroup);

            sequence.SetNode(thereIs);
            sequence.SetNode(parallelSelector_1);

            return sequence;
        }
        #endregion

        private void OnDrawGizmos()
        {
            if (!ShowGizmos) return;

            Gizmos.color = MaxRangeToSee;
            Gizmos.DrawWireSphere(transform.position, rangeToSeeTarget.y);
            Gizmos.color = MinRangeToSee;
            Gizmos.DrawWireSphere(transform.position, rangeToSeeTarget.x);
        }
    }
}