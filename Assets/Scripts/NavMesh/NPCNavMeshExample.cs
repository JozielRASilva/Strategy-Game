using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class NPCNavMeshExample : MonoBehaviour
{

    public SOAttributes attributes;
    public float timeToRun = 2;
    public float Distance = 1;

    public TargetController targetController;

    public NavMeshController navMeshController;

    BehaviourTree behaviourTree;

    void Start()
    {
        SetBehaviour();
    }

    public void SetBehaviour()
    {
        if (!behaviourTree)
            behaviourTree = gameObject.AddComponent<BehaviourTree>();

        if(!attributes) return;

        BTSequence collect = new BTSequence();
        collect.SetNode(new BTMoveByNavMesh(navMeshController, targetController, attributes.speed, attributes.distance));

        behaviourTree.Build(collect);

    }

}
