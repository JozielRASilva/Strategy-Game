using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ZombieDiorama.Character.Behaviours;
using ZombieDiorama.Character.Behaviours.Custom;
using ZombieDiorama.Character.Behaviours.Zombie;
using ZombieDiorama.Character.Behaviours.Decorators;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Utilities.Events;
using Sirenix.OdinInspector;
using ZombieDiorama.Character.Info;
using UnityEngine.Serialization;

namespace ZombieDiorama.Character.AIs
{
    public class Zombie : AIBase
    {
        [TitleGroup("Geral Info")]
        public SOZombieAttributes ZombieAttributes;

        [Header("Patrol")]
        public Transform[] Waypoints;

        [Header("Attack")]
        public GameObject Hitboxes;
        public EventCaller OnAttackEvent;

        [Header("Call Zombies")]
        public GameObject CallCounter;
        public EventCaller OnCallEvent;

        [TitleGroup("Gizmos")]
        public float GizmosSize;


        public override void SetBehaviour()
        {
            BTParallelSelector parallelSelectorCheckingMove = new BTParallelSelector();
            parallelSelectorCheckingMove.SetNode(new BTMoveByNavMesh(navMeshHandler, targetHandler, ZombieAttributes.Speed, ZombieAttributes.DistanceToPatrolTarget));
            parallelSelectorCheckingMove.SetNode(new BTChasingSoldier(targetHandler, ZombieAttributes.MinDistance, ZombieAttributes.MaxDistance));

            BTSequence sequenceChasing = new BTSequence();
            sequenceChasing.SetNode(parallelSelectorCheckingMove);
            sequenceChasing.SetNode(new BTZombieAttack(Hitboxes, ZombieAttributes.CoolDown, OnAttackEvent));

            BTSequence sequenceSeeTarget = new BTSequence();
            sequenceSeeTarget.SetNode(new BTSeeSoldier(targetHandler, ZombieAttributes.DistanceToTarget, ZombieAttributes.TargetTag.Value));
            sequenceSeeTarget.SetNode(new BTCallHorde(CallCounter, ZombieAttributes.TimeCalling, OnCallEvent));
            sequenceSeeTarget.SetNode(sequenceChasing);

            BTInverter inverter = new BTInverter();
            inverter.SetNode(new BTWasCalled(targetHandler, ZombieAttributes.ListeningField, ZombieAttributes.CallCounterTag.Value));

            BTParallelSelector parallelSelectorUpdateTarget = new BTParallelSelector();
            parallelSelectorUpdateTarget.SetNode(inverter);
            parallelSelectorUpdateTarget.SetNode(new BTSeeSoldier(targetHandler, ZombieAttributes.DistanceToTarget, ZombieAttributes.TargetTag.Value));
            parallelSelectorUpdateTarget.SetNode(new BTMoveByNavMesh(navMeshHandler, targetHandler, ZombieAttributes.Speed, ZombieAttributes.DistanceToTarget));

            BTSequence sequenceCalled = new BTSequence();
            sequenceCalled.SetNode(new BTWasCalled(targetHandler, ZombieAttributes.ListeningField, ZombieAttributes.CallCounterTag.Value));
            sequenceCalled.SetNode(parallelSelectorUpdateTarget);

            BTSequence sequencePatrol = new BTSequence();
            sequencePatrol.SetNode(new BTCheckWaypoint(ZombieAttributes.DistanceToWaypoint, targetHandler));
            sequencePatrol.SetNode(new BTRealignWaypoint(Waypoints, targetHandler));

            BTParallelSelector parallelSelectorChecking = new BTParallelSelector();
            parallelSelectorChecking.SetNode(new BTSeeSoldier(targetHandler, ZombieAttributes.DistanceToTarget, ZombieAttributes.TargetTag.Value));
            parallelSelectorChecking.SetNode(new BTWasCalled(targetHandler, ZombieAttributes.ListeningField, ZombieAttributes.CallCounterTag.Value));
            parallelSelectorChecking.SetNode(new BTMoveByNavMesh(navMeshHandler, targetHandler, ZombieAttributes.Speed, ZombieAttributes.DistanceToPatrolTarget));

            BTSelector selectorRoutine = new BTSelector();
            selectorRoutine.SetNode(sequencePatrol);
            selectorRoutine.SetNode(parallelSelectorChecking);

            BTSelector selectorStart = new BTSelector();
            selectorStart.SetNode(sequenceSeeTarget);
            selectorStart.SetNode(sequenceCalled);
            selectorStart.SetNode(selectorRoutine);

            behaviourTree.Build(selectorStart);
        }

        private void OnDrawGizmos()
        {
            if (Waypoints != null)
            {
                for (int i = 0; i < Waypoints.Length; i++)
                {
                    if (!Waypoints[i])
                        continue;
                    if (i + 1 < Waypoints.Length)
                        Gizmos.DrawLine(Waypoints[i].position, Waypoints[i + 1].position);
                    else
                        Gizmos.DrawLine(Waypoints[i].position, Waypoints[0].position);

                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(Waypoints[i].position, GizmosSize);
                }
            }
        }
    }
}