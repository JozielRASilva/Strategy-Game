using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AISupportSoldier : MonoBehaviour
{
    public SOAttributes attributes;

    public float distanceToSet = 0.5f;
    public float delayToSet = 0f;

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

        if (!attributes) return;

        BTSequence root = new BTSequence();

        // SET OBJECT
        BTNode branchSetObject = GetBranchSetObject();

        // REAGROUP

        // FOLLOW LEADER

        root.SetNode(branchSetObject);

        behaviourTree.Build(root);

    }

    #region SET OBJECT
    public BTNode GetBranchSetObject()
    {
        BTSequence sequence_setObject = new BTSequence();

        BTObjectToSet thereIsObjectToSet = new BTObjectToSet();

        BTUpdateObjectToSet updateObjectToSet = new BTUpdateObjectToSet(targetController);


        BTSequence sequence_1 = new BTSequence();
        BTParallelSelector parallelSelector_1 = new BTParallelSelector();

        BTNextToTarget nextToTarget = new BTNextToTarget(targetController, distanceToSet);
        BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(navMeshController, targetController, attributes.speed, distanceToSet);

        parallelSelector_1.SetNode(nextToTarget);
        parallelSelector_1.SetNode(moveToSet);

        BTSetObject setObject = new BTSetObject(delayToSet);

        sequence_1.SetNode(parallelSelector_1);
        sequence_1.SetNode(setObject);


        sequence_setObject.SetNode(thereIsObjectToSet);
        sequence_setObject.SetNode(updateObjectToSet);
        sequence_setObject.SetNode(sequence_1);

        return sequence_setObject;
    }
    #endregion


}
