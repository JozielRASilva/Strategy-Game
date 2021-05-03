using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class NPC : MonoBehaviour
{

    public SOAttributes attributes;

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

        BTSequence combat = new BTSequence();
        combat.SetNode(new BTSee(attributes.enemy, attributes.rangeToCheckEnemy));
        combat.SetNode(new BTCombat(attributes.enemy, attributes.coolDown, attributes.projectile));
        combat.SetNode(new BTDodge(attributes.dodgeThis, attributes.dodgeTimeRange));

        BTParallelSelector parallelSelector = new BTParallelSelector();
        parallelSelector.SetNode(new BTSee(attributes.enemy, attributes.rangeToCheckEnemy));
        parallelSelector.SetNode(new BTMoveTo(attributes.target, attributes.speed, attributes.distance));

        BTSequence collect = new BTSequence();
        collect.SetNode(new BTThereIs(attributes.target));
        collect.SetNode(parallelSelector);
        collect.SetNode(new BTCollect(attributes.target, attributes.distanceToCollect));

        BTSelector selector = new BTSelector();
        selector.SetNode(combat);
        selector.SetNode(collect);

        behaviourTree.Build(selector);


        GetComponent<MeshRenderer>().material.color = attributes.color;
    }

}
