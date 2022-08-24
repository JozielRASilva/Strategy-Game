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
using UnityEngine.Serialization;

namespace ZombieDiorama.Character.AIs
{
    public class AISupportSoldier : Soldier
    {
        [TitleGroup("Geral Info")]
        public SOSupportAttributes SupportAttributes;

        [Title("AI Set Object Effect")]
        public EventCaller SetObjectEffect;
        public EventCaller SettingObjectEffect;

        [Title("Heal")]
        [FormerlySerializedAs("healHitBox")]public GameObject HealHitBox;

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
            BTUpdateObjectToSet updateObjectToSet = new BTUpdateObjectToSet(targetHandler);
            #endregion

            #region Moving
            BTSequence sequence_1 = new BTSequence();
            BTParallelSelector parallelSelector_1 = new BTParallelSelector();

            BTNextToTarget nextToTarget = new BTNextToTarget(targetHandler, SupportAttributes.DistanceToSet);
            BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(navMeshHandler, targetHandler, SoldierAttributes.Speed, SupportAttributes.DistanceToSet);

            parallelSelector_1.SetNode(nextToTarget);
            parallelSelector_1.SetNode(moveToSet);

            BTSetObject setObject = new BTSetObject(SupportAttributes.DelayToSet, SettingObjectEffect, SetObjectEffect);

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

            BTMemberToHeal memberToHeal = new BTMemberToHeal(squadMember, targetHandler);

            BTMoveByNavMesh MoveToHeal = new BTMoveByNavMesh(navMeshHandler, targetHandler, SoldierAttributes.Speed, SupportAttributes.DistanceToHeal);

            BTHitbox hitboxHeal = new BTHitbox(HealHitBox, SupportAttributes.HealCooldown, SupportAttributes.HealRest, targetHandler, SupportAttributes.DampingToHeal, HealEffect);

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
            parallel.SetNode(new BTMoveByNavMesh(navMeshHandler, targetHandler, SoldierAttributes.Speed, SoldierAttributes.RangeToSeeTarget.x));
            parallel.SetNode(new BTCloseToTarget(targetHandler, SoldierAttributes.RangeToSeeTarget.x, SoldierAttributes.RangeToSeeTarget.y));
            BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(targetHandler, SoldierAttributes.DistanceToRegroup);
            BTMemberToHeal memberToHeal = new BTMemberToHeal(squadMember, targetHandler);
            BTObjectToSet thereIsObjectToSet = new BTObjectToSet(squadMember);

            parallel.SetNode(thereIsObjectToSet);
            parallel.SetNode(calledToRegroup);
            parallel.SetNode(memberToHeal);

            BTInverter inverter = new BTInverter();
            BTSeeZombie seeZombie = new BTSeeZombie(targetHandler, SoldierAttributes.RangeToSeeTarget.y, SoldierAttributes.TargetTag.Value);
            inverter.SetNode(seeZombie);
            parallel.SetNode(inverter);

            BTSequence sequence_1 = new BTSequence();
            sequence_1.SetNode(parallel);
            sequence_1.SetNode(new BTSoldierAttack(targetHandler, SoldierAttributes.ShootCooldown, shootHandler, SoldierAttributes.LookAtZombieDamping, SoldierAttributes.TargetTag.Value, AttackEventCaller));

            BTSequence sequence = new BTSequence();
            sequence.SetNode(new BTSeeZombie(targetHandler, SoldierAttributes.RangeToSeeTarget.y, SoldierAttributes.TargetTag.Value));
            sequence.SetNode(sequence_1);

            BTSelector selector = new BTSelector();
            selector.SetNode(sequence);

            return selector;
        }
        #endregion
    }
}