using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Character.Handler.Team;
using ZombieDiorama.Character.Behaviours;
using ZombieDiorama.Character.Behaviours.Soldier;
using ZombieDiorama.Character.Behaviours.Custom;
using ZombieDiorama.Character.Behaviours.Decorators;
using ZombieDiorama.Utilities.Events;
using ZombieDiorama.Character.Info;

namespace ZombieDiorama.Character.AIs
{
    public class Soldier : AIBase
    {
        [TitleGroup("Geral Info"), SerializeField]
        protected SOSoldierAttributes soldierAttributes;
        
        [Title("Fight")]
        public GameObject muzzle;
        public GameObject bullet;

        [Title("AI Attack Events")]
        public EventCaller AttackEventCaller;

        [Title("Gizmos")]
        public bool ShowGizmos = true;
        public Color MaxRangeToSee = Color.blue;
        public Color MinRangeToSee = Color.cyan;

        protected SquadMember squadMember;
        protected ShootHandler shootHandler;

        #region  SETUP
        protected override void Awake()
        {
            base.Awake();
            squadMember = GetComponent<SquadMember>();
            shootHandler = GetComponent<ShootHandler>();
        }

        public override void SetBehaviour()
        {
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

        #endregion

        #region FIGHT
        public virtual BTNode GetBranchFight()
        {
            BTParallelSelector parallel = new BTParallelSelector();
            parallel.SetNode(new BTMoveByNavMesh(NavMeshHandler, TargetHandler, soldierAttributes.Speed, soldierAttributes.RangeToSeeTarget.x));
            parallel.SetNode(new BTCloseToTarget(TargetHandler, soldierAttributes.RangeToSeeTarget.x, soldierAttributes.RangeToSeeTarget.y));
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(TargetHandler, soldierAttributes.DistanceToRegroup);
            parallel.SetNode(calledToRegroup);

            BTInverter inverter = new BTInverter();
            BTSeeZombie seeZombie = new BTSeeZombie(TargetHandler, soldierAttributes.RangeToSeeTarget.y, soldierAttributes.TargetTag.Value);
            inverter.SetNode(seeZombie);
            parallel.SetNode(inverter);

            BTSequence sequence_1 = new BTSequence();
            sequence_1.SetNode(parallel);
            sequence_1.SetNode(new BTSoldierAttack(TargetHandler, soldierAttributes.ShootCooldown, shootHandler, soldierAttributes.LookAtZombieDamping, soldierAttributes.TargetTag.Value, AttackEventCaller));

            BTSequence sequence = new BTSequence();
            sequence.SetNode(new BTSeeZombie(TargetHandler, soldierAttributes.RangeToSeeTarget.y, soldierAttributes.TargetTag.Value));
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
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(TargetHandler, soldierAttributes.DistanceToRegroup);
            BTUpdateRegroup updateRegroup = new BTUpdateRegroup(TargetHandler);
            #endregion

            #region Moving
            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTNextToTarget nextToTarget = new BTNextToTarget(TargetHandler, soldierAttributes.DistanceToRegroup);
            BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(NavMeshHandler, TargetHandler, soldierAttributes.Speed, soldierAttributes.DistanceToRegroup);

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
            BTThereIs thereIs = new BTThereIs(TargetHandler, soldierAttributes.TargetTag.Value);

            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTSee see = new BTSee(TargetHandler, soldierAttributes.TargetTag.Value, soldierAttributes.RangeToSeeTarget.y);
            BTMoveByNavMesh moveTo = new BTMoveByNavMesh(NavMeshHandler, TargetHandler, soldierAttributes.Speed, soldierAttributes.RangeToSeeTarget.x);

            parallelSelector_1.SetNode(see);
            parallelSelector_1.SetNode(moveTo);

            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(squadMember);
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(TargetHandler, soldierAttributes.DistanceToRegroup);
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
            BTUpdateLeader updateLeader = new BTUpdateLeader(squadMember, TargetHandler);
            sequence_2.SetNode(hasLeader);
            sequence_2.SetNode(updateLeader);

            BTParallelSelector parallelSelector_2 = new BTParallelSelector();
            sequence_2.SetNode(parallelSelector_2);

            BTSee see_2 = new BTSee(TargetHandler, soldierAttributes.TargetTag.Value, soldierAttributes.DistanceToTarget, true);
            BTMoveByNavMesh moveLeader = new BTMoveByNavMesh(NavMeshHandler, TargetHandler, soldierAttributes.Speed, soldierAttributes.DistanceToLeader);

            parallelSelector_2.SetNode(see_2);
            parallelSelector_2.SetNode(moveLeader);

            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(squadMember);
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(TargetHandler, soldierAttributes.DistanceToRegroup);
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
            BTThereIs thereIs = new BTThereIs(TargetHandler, soldierAttributes.TargetTag.Value);

            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTSee see = new BTSee(TargetHandler, soldierAttributes.TargetTag.Value, soldierAttributes.RangeToSeeTarget.y);
            BTMoveByNavMesh moveTo = new BTMoveByNavMesh(NavMeshHandler, TargetHandler, soldierAttributes.Speed, soldierAttributes.RangeToSeeTarget.x);

            parallelSelector_1.SetNode(see);
            parallelSelector_1.SetNode(moveTo);

            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(squadMember);
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(TargetHandler, soldierAttributes.DistanceToRegroup);
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
            Gizmos.DrawWireSphere(transform.position, soldierAttributes.RangeToSeeTarget.y);
            Gizmos.color = MinRangeToSee;
            Gizmos.DrawWireSphere(transform.position, soldierAttributes.RangeToSeeTarget.x);
        }
    }
}