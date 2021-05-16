using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AISupportSoldier : MonoBehaviour
{
    public SOAttributes attributes;

    public float distanceToSet = 0.5f;
    public float delayToSet = 0f;

    public float distanceToRegroup = 1;

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

        BTSelector root = new BTSelector();

        // SET OBJECT
        BTNode branchSetObject = GetBranchSetObject();

        // REGROUP
        BTNode branchRegroup = GetBranchRegroup();

        // FOLLOW LEADER

        root.SetNode(branchSetObject);
        root.SetNode(branchRegroup);

        behaviourTree.Build(root);

    }

    #region SET OBJECT
    public BTNode GetBranchSetObject()
    {
        BTSequence sequence_setObject = new BTSequence();

        #region Cheking
        BTObjectToSet thereIsObjectToSet = new BTObjectToSet();
        BTUpdateObjectToSet updateObjectToSet = new BTUpdateObjectToSet(targetController);
        #endregion

        #region Moving
        BTSequence sequence_1 = new BTSequence();
        BTParallelSelector parallelSelector_1 = new BTParallelSelector();

        BTNextToTarget nextToTarget = new BTNextToTarget(targetController, distanceToSet);
        BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(navMeshController, targetController, attributes.speed, distanceToSet);

        parallelSelector_1.SetNode(nextToTarget);
        parallelSelector_1.SetNode(moveToSet);

        BTSetObject setObject = new BTSetObject(delayToSet);

        sequence_1.SetNode(parallelSelector_1);
        sequence_1.SetNode(setObject);
        #endregion

        #region Apply Nodes
        sequence_setObject.SetNode(thereIsObjectToSet);
        sequence_setObject.SetNode(updateObjectToSet);
        sequence_setObject.SetNode(sequence_1);
        #endregion

        return sequence_setObject;
    }
    #endregion

#region REAGROUP
    public BTNode GetBranchRegroup()
    {
        BTSequence sequence_regroup = new BTSequence();

        #region Cheking
        BTCalledToRegroup calledToRegroup = new BTCalledToRegroup(targetController, distanceToRegroup);
        BTUpdateRegroup updateRegroup = new BTUpdateRegroup(targetController);
        #endregion

        #region Moving
        BTParallelSelector parallelSelector_1 = new BTParallelSelector();

        BTNextToTarget nextToTarget = new BTNextToTarget(targetController, distanceToSet);
        BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(navMeshController, targetController, attributes.speed, distanceToSet);

        parallelSelector_1.SetNode(nextToTarget);
        parallelSelector_1.SetNode(moveToSet);
        #endregion


        #region  Apply Node
        sequence_regroup.SetNode(calledToRegroup);
        sequence_regroup.SetNode(updateRegroup);
        sequence_regroup.SetNode(parallelSelector_1);
        #endregion

        return sequence_regroup;
    }
    #endregion

}
