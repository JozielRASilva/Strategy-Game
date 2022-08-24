using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Controllers.Team;
using ZombieDiorama.Character.Behaviours;
using ZombieDiorama.Character.Behaviours.Soldier;
using ZombieDiorama.Character.Behaviours.Custom;
using ZombieDiorama.Character.Behaviours.Decorators;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.AIs
{
    public class AISupportSoldier : Soldier
    {
        [Title("Set Object")]
        public float distanceToSet = 0.5f;
        public float delayToSet = 0.2f;

        [Title("AI Set Object Effect")]
        public EventCaller SetObjectEffect;
        public EventCaller SettingObjectEffect;

        [Title("Heal")]
        public float distanceToHeal = 0.7f;
        public float dampingToHeal = 1f;

        public GameObject healHitBox;
        public float healCooldown = 2f;
        public float healRest = 1f;

        [Title("AI Heal Effect")]
        public EventCaller HealEffect;

        #region Base Info
        public override void SetBehaviour()
        {
            BTSelector root = new BTSelector();

            // SET OBJECT
            BTNode branchSetObject = GetBranchSetObject();

            // REGROUP
            BTNode branchRegroup = GetBranchRegroup();

            // HEAL
            BTNode branchHeal = GetBranchHeal();

            // FIGHT
            BTNode fight = GetBranchFight();

            // FOLLOW LEADER
            BTNode branchTeam = GetBranchTeam();

            root.SetNode(branchSetObject);
            root.SetNode(branchRegroup);
            root.SetNode(branchHeal);
            root.SetNode(fight);
            root.SetNode(branchTeam);

            behaviourTree.Build(root);
        }
        #endregion

        #region SET OBJECT
        public BTNode GetBranchSetObject()
        {
            BTSequence sequence_setObject = new BTSequence();

            #region Cheking
            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(SquadMember);
            BTUpdateObjectToSet updateObjectToSet = new BTUpdateObjectToSet(TargetController);
            #endregion

            #region Moving
            BTSequence sequence_1 = new BTSequence();
            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTNextToTarget nextToTarget = new BTNextToTarget(TargetController, distanceToSet);
            BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(NavMeshController, TargetController, Speed, distanceToSet);

            parallelSelector_1.SetNode(nextToTarget);
            parallelSelector_1.SetNode(moveToSet);

            BTSetObject setObject = new BTSetObject(delayToSet, SettingObjectEffect, SetObjectEffect);

            sequence_1.SetNode(parallelSelector_1);
            sequence_1.SetNode(setObject);
            #endregion

            #region Apply Nodes
            sequence_setObject.SetNode(thereIsObjectToSet);
            sequence_setObject.SetNode(updateObjectToSet);
            sequence_setObject.SetNode(sequence_1);
            #endregion

            return sequence_setObject;
        }
        #endregion

        #region  HEAL
        private BTNode GetBranchHeal()
        {
            BTSequence sequence = new BTSequence();

            BTMemberToHeal memberToHeal = new BTMemberToHeal(SquadMember, TargetController);

            BTMoveByNavMesh MoveToHeal = new BTMoveByNavMesh(NavMeshController, TargetController, Speed, distanceToHeal);

            BTHitbox hitboxHeal = new BTHitbox(healHitBox, healCooldown, healRest, TargetController, dampingToHeal, HealEffect);

            sequence.SetNode(memberToHeal);
            sequence.SetNode(MoveToHeal);
            sequence.SetNode(hitboxHeal);

            return sequence;
        }
        #endregion

        #region FIGHT
        public override BTNode GetBranchFight()
        {
            BTParallelSelector parallel = new BTParallelSelector();
            parallel.SetNode(new BTMoveByNavMesh(NavMeshController, TargetController, Speed, rangeToSeeTarget.x));
            parallel.SetNode(new BTCloseToTarget(TargetController, rangeToSeeTarget.x, rangeToSeeTarget.y));
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(TargetController, distanceToRegroup);
            BTMemberToHeal memberToHeal = new BTMemberToHeal(SquadMember, TargetController);
            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(SquadMember);

            parallel.SetNode(thereIsObjectToSet);
            parallel.SetNode(calledToRegroup);
            parallel.SetNode(memberToHeal);

            BTInverter inverter = new BTInverter();
            BTSeeZombie seeZombie = new BTSeeZombie(TargetController, rangeToSeeTarget.y, target);
            inverter.SetNode(seeZombie);
            parallel.SetNode(inverter);

            BTSequence sequence_1 = new BTSequence();
            sequence_1.SetNode(parallel);
            sequence_1.SetNode(new BTSoldierAttack(TargetController, shootCooldown, ShootHandler, lookAtZombieDamping, target, AttackEventCaller));

            BTSequence sequence = new BTSequence();
            sequence.SetNode(new BTSeeZombie(TargetController, rangeToSeeTarget.y, target));
            sequence.SetNode(sequence_1);

            BTSelector selector = new BTSelector();
            selector.SetNode(sequence);

            return selector;
        }
        #endregion
    }
}