using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ZombieDiorama.Character.Behaviours;
using ZombieDiorama.Character.Behaviours.Custom;
using ZombieDiorama.Character.Behaviours.Zombie;
using ZombieDiorama.Character.Behaviours.Decorators;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.AIs
{
    public class Zombie : AIBase
    {

        [Header("Patrol")]
        public Transform[] waypoints;
        public float gizmoSize;
        public float speed;
        public float distanceToTarget;
        public float distanceToWaypoint;

        [Header("Attack")]
        public GameObject hitboxes;
        public float coolDown;

        [Header("Attack Events")]
        public EventCaller OnAttackEvent;

        [Header("Call Zombies")]
        public GameObject callCounter;
        public float listeningField;
        public float distanceToZombie;
        public float timeCalling;

        [Header("Call Events")]
        public EventCaller OnCallEvent;

        [Header("Zombie Field of View")]
        public float distanceView;

        [Header("Chasing Soldier FOV")]
        public float minDistance;
        public float maxDistance;

        public override void SetBehaviour()
        {
            BTParallelSelector parallelSelectorCheckingMove = new BTParallelSelector();
            parallelSelectorCheckingMove.SetNode(new BTMoveByNavMesh(NavMeshController, TargetController, speed, distanceToTarget));
            parallelSelectorCheckingMove.SetNode(new BTChasingSoldier(TargetController, minDistance, maxDistance));

            BTSequence sequenceChasing = new BTSequence();
            sequenceChasing.SetNode(parallelSelectorCheckingMove);
            sequenceChasing.SetNode(new BTZombieAttack(hitboxes, coolDown, OnAttackEvent));

            BTSequence sequenceSeeTarget = new BTSequence();
            sequenceSeeTarget.SetNode(new BTSeeSoldier(TargetController, distanceView));
            sequenceSeeTarget.SetNode(new BTCallHorde(callCounter, timeCalling, OnCallEvent));
            sequenceSeeTarget.SetNode(sequenceChasing);

            BTInverter inverter = new BTInverter();
            inverter.SetNode(new BTWasCalled(TargetController, listeningField));

            BTParallelSelector parallelSelectorUpdateTarget = new BTParallelSelector();
            parallelSelectorUpdateTarget.SetNode(inverter);
            parallelSelectorUpdateTarget.SetNode(new BTSeeSoldier(TargetController, distanceView));
            parallelSelectorUpdateTarget.SetNode(new BTMoveByNavMesh(NavMeshController, TargetController, speed, distanceToZombie));

            BTSequence sequenceCalled = new BTSequence();
            sequenceCalled.SetNode(new BTWasCalled(TargetController, listeningField));
            sequenceCalled.SetNode(parallelSelectorUpdateTarget);

            BTSequence sequencePatrol = new BTSequence();
            sequencePatrol.SetNode(new BTCheckWaypoint(distanceToWaypoint, TargetController));
            sequencePatrol.SetNode(new BTRealignWaypoint(waypoints, TargetController));

            BTParallelSelector parallelSelectorChecking = new BTParallelSelector();
            parallelSelectorChecking.SetNode(new BTSeeSoldier(TargetController, distanceView));
            parallelSelectorChecking.SetNode(new BTWasCalled(TargetController, listeningField));
            parallelSelectorChecking.SetNode(new BTMoveByNavMesh(NavMeshController, TargetController, speed, distanceToTarget));

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
            if (waypoints != null)
            {
                for (int i = 0; i < waypoints.Length; i++)
                {
                    if (i + 1 < waypoints.Length)
                        Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                    else
                        Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);

                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(waypoints[i].position, gizmoSize);
                }
            }
        }
    }
}