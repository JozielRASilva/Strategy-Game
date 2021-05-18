using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    BehaviourTree behaviourTree;

    private NavMeshController navMesh;
    public TargetController target;

    public GameObject muzzle;
    public GameObject bullet;
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

        BTParallelSelector parallel = new BTParallelSelector();
        parallel.SetNode(new BTMoveTo("Zombie", 2, 1));
        parallel.SetNode(new BTCloseToTarget(target, 5, 20));


        BTSequence sequence_1 = new BTSequence();
        sequence_1.SetNode(parallel);
        sequence_1.SetNode(new BTSoldierAttack(target, 0.5f, bullet, muzzle));


        BTSequence sequence = new BTSequence();
        sequence.SetNode(new BTSeeZombie(target, 20));
        sequence.SetNode(sequence_1);

        BTSelector selector = new BTSelector();
        selector.SetNode(sequence);

        behaviourTree.Build(selector);
    }
}
