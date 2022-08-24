using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public class AISupportSoldier : Soldier
    {
        [TitleGroup("Geral Info"), SerializeField]
        protected SOSupportAttributes supportAttributes;

        [Title("AI Set Object Effect")]
        public EventCaller SetObjectEffect;
        public EventCaller SettingObjectEffect;

        [Title("Heal")]
        public GameObject healHitBox;

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
            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(squadMember);
            BTUpdateObjectToSet updateObjectToSet = new BTUpdateObjectToSet(TargetHandler);
            #endregion

            #region Moving
            BTSequence sequence_1 = new BTSequence();
            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTNextToTarget nextToTarget = new BTNextToTarget(TargetHandler, supportAttributes.DistanceToSet);
            BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(NavMeshHandler, TargetHandler, soldierAttributes.Speed, supportAttributes.DistanceToSet);

            parallelSelector_1.SetNode(nextToTarget);
            parallelSelector_1.SetNode(moveToSet);

            BTSetObject setObject = new BTSetObject(supportAttributes.DelayToSet, SettingObjectEffect, SetObjectEffect);

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

            BTMemberToHeal memberToHeal = new BTMemberToHeal(squadMember, TargetHandler);

            BTMoveByNavMesh MoveToHeal = new BTMoveByNavMesh(NavMeshHandler, TargetHandler, soldierAttributes.Speed, supportAttributes.DistanceToHeal);

            BTHitbox hitboxHeal = new BTHitbox(healHitBox, supportAttributes.HealCooldown, supportAttributes.HealRest, TargetHandler, supportAttributes.DampingToHeal, HealEffect);

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
            parallel.SetNode(new BTMoveByNavMesh(NavMeshHandler, TargetHandler, soldierAttributes.Speed, soldierAttributes.RangeToSeeTarget.x));
            parallel.SetNode(new BTCloseToTarget(TargetHandler, soldierAttributes.RangeToSeeTarget.x, soldierAttributes.RangeToSeeTarget.y));
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(TargetHandler, soldierAttributes.DistanceToRegroup);
            BTMemberToHeal memberToHeal = new BTMemberToHeal(squadMember, TargetHandler);
            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(squadMember);

            parallel.SetNode(thereIsObjectToSet);
            parallel.SetNode(calledToRegroup);
            parallel.SetNode(memberToHeal);

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
    }
}