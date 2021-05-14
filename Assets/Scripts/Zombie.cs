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

    public Transform soldierPosition;

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
        parallel_1.SetNode(new BTChasingSoldier(target, soldierPosition));

        BTSequence sequence_ = new BTSequence();
        sequence_.SetNode(parallel_1);

        BTSequence sequence_1 = new BTSequence();
        sequence_1.SetNode(new BTSeeSoldier(soldierPosition));
        sequence_1.SetNode(new BTCallHorde());
        sequence_1.SetNode(sequence_);


    

        BTSequence patrol = new BTSequence();
        patrol.SetNode(new BTCheckWaypoint(1, target));
        patrol.SetNode(new BTRealignWaypoint(waypoints, target));

        BTParallelSelector parallel = new BTParallelSelector();
        parallel.SetNode(sequence_1);
        parallel.SetNode(new BTMoveByNavMesh(navMesh, target, 2, 1));

        BTSelector selector = new BTSelector();
        selector.SetNode(patrol);
        selector.SetNode(parallel);

        behaviourTree.Build(selector);
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
