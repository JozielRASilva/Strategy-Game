using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public Transform[] waypoints;

    BehaviourTree behaviourTree;

    public float gizmoSize;

    private NavMeshController navMesh;
    public TargetController target;

    public GameObject hitboxes;

    public GameObject callCounter;

    public bool calling;

    void Start()
    {
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

        BTParallelSelector parallel_1 = new BTParallelSelector();
        parallel_1.SetNode(new BTMoveByNavMesh(navMesh, target, 2, 1));
        parallel_1.SetNode(new BTChasingSoldier(target, 2, 15));


        BTSequence sequence_ = new BTSequence();
        sequence_.SetNode(parallel_1);
        sequence_.SetNode(new BTZombieAttack(hitboxes, 0.5f));


        BTSequence sequence_1 = new BTSequence();
        sequence_1.SetNode(new BTSeeSoldier(target, 10));
        sequence_1.SetNode(new BTCallHorde(callCounter, calling, 2));
        sequence_1.SetNode(sequence_);

        BTSequence sequence_2 = new BTSequence();
        sequence_2.SetNode(new BTWasCalled(target, calling));
        sequence_2.SetNode(new BTMoveByNavMesh(navMesh, target, 2, 1));


        BTSequence patrol = new BTSequence();
        patrol.SetNode(new BTCheckWaypoint(1, target));
        patrol.SetNode(new BTRealignWaypoint(waypoints, target));

        BTParallelSelector parallel = new BTParallelSelector();
        parallel.SetNode(new BTSeeSoldier(target, 10));
        parallel.SetNode(new BTWasCalled(target, calling));
        parallel.SetNode(new BTMoveByNavMesh(navMesh, target, 2, 1));

        BTSelector selector = new BTSelector();
        selector.SetNode(patrol);
        selector.SetNode(parallel);

        BTSelector selector_1 = new BTSelector();
        selector_1.SetNode(sequence_1);
        selector_1.SetNode(sequence_2);
        selector_1.SetNode(selector);

        behaviourTree.Build(selector_1);
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
