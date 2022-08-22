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
        public float DistanceToSet = 0.5f;
        public float DelayToSet = 0.2f;

        [Title("AI Set Object Effect")]
        public EventCaller SetObjectEffect;
        public EventCaller SettingObjectEffect;

        [Title("Heal")]
        public float DistanceToHeal = 0.7f;
        public float DampingToHeal = 1f;

        public GameObject HealHitBox;
        public float HealCooldown = 2f;
        public float HealRest = 1f;

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

            _behaviourTree.Build(root);
        }
        #endregion

        #region SET OBJECT
        public BTNode GetBranchSetObject()
        {
            BTSequence sequence_setObject = new BTSequence();

            #region Cheking
            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(SquadMember);
            BTUpdateObjectToSet updateObjectToSet = new BTUpdateObjectToSet(_targetController);
            #endregion

            #region Moving
            BTSequence sequence_1 = new BTSequence();
            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTNextToTarget nextToTarget = new BTNextToTarget(_targetController, DistanceToSet);
            BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(_navMeshController, _targetController, Speed, DistanceToSet);

            parallelSelector_1.SetNode(nextToTarget);
            parallelSelector_1.SetNode(moveToSet);

            BTSetObject setObject = new BTSetObject(DelayToSet, SettingObjectEffect, SetObjectEffect);

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

            BTMemberToHeal memberToHeal = new BTMemberToHeal(SquadMember, _targetController);

            BTMoveByNavMesh MoveToHeal = new BTMoveByNavMesh(_navMeshController, _targetController, Speed, DistanceToHeal);

            BTHitbox hitboxHeal = new BTHitbox(HealHitBox, HealCooldown, HealRest, _targetController, DampingToHeal, HealEffect);

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
            parallel.SetNode(new BTMoveByNavMesh(_navMeshController, _targetController, Speed, rangeToSeeTarget.x));
            parallel.SetNode(new BTCloseToTarget(_targetController, rangeToSeeTarget.x, rangeToSeeTarget.y));
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(_targetController, distanceToRegroup);
            BTMemberToHeal memberToHeal = new BTMemberToHeal(SquadMember, _targetController);
            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(SquadMember);

            parallel.SetNode(thereIsObjectToSet);
            parallel.SetNode(calledToRegroup);
            parallel.SetNode(memberToHeal);

            BTInverter inverter = new BTInverter();
            BTSeeZombie seeZombie = new BTSeeZombie(_targetController, rangeToSeeTarget.y);
            inverter.SetNode(seeZombie);
            parallel.SetNode(inverter);

            BTSequence sequence_1 = new BTSequence();
            sequence_1.SetNode(parallel);
            sequence_1.SetNode(new BTSoldierAttack(_targetController, shootCooldown, ShootHandler, lookAtZombieDamping, target, AttackEventCaller));

            BTSequence sequence = new BTSequence();
            sequence.SetNode(new BTSeeZombie(_targetController, rangeToSeeTarget.y));
            sequence.SetNode(sequence_1);

            BTSelector selector = new BTSelector();
            selector.SetNode(sequence);

            return selector;
        }
        #endregion
    }
}