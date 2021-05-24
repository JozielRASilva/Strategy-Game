using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour, AIBase
{
    BehaviourTree behaviourTree;
    private NavMeshController navMesh;

    [Header("Patrol")]
    public Transform[] waypoints;
    public float gizmoSize;
    public TargetController target;
    public float speed;
    public float distanceToTarget;
    public float distanceToWaypoint;

    [Header("Attack")]
    public GameObject hitboxes;
    public float coolDown;

    [Header("Call Zombies")]
    public GameObject callCounter;
    public float listeningField;
    public float distanceToZombie;
    public float timeCalling;

    [Header("Zombie Field of View")]
    public float distanceView;

    [Header("Chasing Soldier FOV")]
    public float minDistance;
    public float maxDistance;

    void Start()
    {
        navMesh = gameObject.GetComponent<NavMeshController>();
        target = gameObject.GetComponent<TargetController>();
        SetBehaviour();
    }

    public void SetBehaviour()
    {
        if (!behaviourTree)
        {
            behaviourTree = gameObject.AddComponent<BehaviourTree>();
        }

        if (!navMesh)
        {
            navMesh = gameObject.AddComponent<NavMeshController>();
        }

        if (!target)                                                                                                                                                                                                                                                                                                                                                                       
        {
            target = gameObject.AddComponent<TargetController>();
        }


        BTParallelSelector parallelSelectorCheckingMove = new BTParallelSelector();
        parallelSelectorCheckingMove.SetNode(new BTMoveByNavMesh(navMesh, target, speed, distanceToTarget));
        parallelSelectorCheckingMove.SetNode(new BTChasingSoldier(target, minDistance, maxDistance));


        BTSequence sequenceChasing = new BTSequence();
        sequenceChasing.SetNode(parallelSelectorCheckingMove);
        sequenceChasing.SetNode(new BTZombieAttack(hitboxes, coolDown));


        BTSequence sequenceSeeTarget = new BTSequence();
        sequenceSeeTarget.SetNode(new BTSeeSoldier(target, distanceView));
        sequenceSeeTarget.SetNode(new BTCallHorde(callCounter, timeCalling));
        sequenceSeeTarget.SetNode(sequenceChasing);

        BTInverter inverter = new BTInverter();
        inverter.SetNode(new BTWasCalled(target, listeningField));

        BTParallelSelector parallelSelectorUpdateTarget = new BTParallelSelector();
        parallelSelectorUpdateTarget.SetNode(inverter);
        parallelSelectorUpdateTarget.SetNode(new BTSeeSoldier(target, distanceView));
        parallelSelectorUpdateTarget.SetNode(new BTMoveByNavMesh(navMesh, target, speed, distanceToZombie));

        BTSequence sequenceCalled = new BTSequence();
        sequenceCalled.SetNode(new BTWasCalled(target, listeningField));
        sequenceCalled.SetNode(parallelSelectorUpdateTarget);

        BTSequence sequencePatrol = new BTSequence();
        sequencePatrol.SetNode(new BTCheckWaypoint(distanceToWaypoint, target));
        sequencePatrol.SetNode(new BTRealignWaypoint(waypoints, target));

        BTParallelSelector parallelSelectorChecking = new BTParallelSelector();
        parallelSelectorChecking.SetNode(new BTSeeSoldier(target, distanceView));
        parallelSelectorChecking.SetNode(new BTWasCalled(target, listeningField));
        parallelSelectorChecking.SetNode(new BTMoveByNavMesh(navMesh, target, speed, distanceToTarget));

        BTSelector selectorRoutine = new BTSelector();
        selectorRoutine.SetNode(sequencePatrol);
        selectorRoutine.SetNode(parallelSelectorChecking);

        BTSelector selectorStart = new BTSelector();
        selectorStart.SetNode(sequenceSeeTarget);
        selectorStart.SetNode(sequenceCalled);
        selectorStart.SetNode(selectorRoutine);

        behaviourTree.Build(selectorStart);
    }


    public void RestartBehaviour()
    {

        SetBehaviour();
        if (behaviourTree)
        {
            behaviourTree.enabled = true;
            behaviourTree.Initialize();
        }
    }

    public void StopBehaviour()
    {
        if (behaviourTree)
        {
            behaviourTree.Stop();
            behaviourTree.enabled = false;

            behaviourTree.StopAllCoroutines();
        }
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
