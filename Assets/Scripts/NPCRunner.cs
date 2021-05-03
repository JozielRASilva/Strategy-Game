using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class NPCRunner : MonoBehaviour
{

    public SOAttributes attributes;
    public float timeToRun = 2;
    public float Distance = 1;

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
        collect.SetNode(new BTSee(attributes.enemy, Distance));
        collect.SetNode(new BTFugirInimigo(attributes.enemy, attributes.speed, attributes.distance));

        behaviourTree.Build(collect);


        GetComponent<MeshRenderer>().material.color = attributes.color;
    }

}
