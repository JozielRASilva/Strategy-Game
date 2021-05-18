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

    public float distanceToTarget = 1;

    public float distanceToLeader = 1;

    public string target = "Zumbi";

    public TargetController targetController;

    public NavMeshController navMeshController;

    public SquadMember squadMember;

    private BehaviourTree behaviourTree;

    private void Awake()
    {
        targetController = GetComponent<TargetController>();

        navMeshController = GetComponent<NavMeshController>();

        squadMember = GetComponent<SquadMember>();
    }

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
        BTNode branchTeam = GetBranchTeam();

        root.SetNode(branchSetObject);
        root.SetNode(branchRegroup);
        root.SetNode(branchTeam);

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


    #region TEAM
    public BTNode GetBranchTeam()
    {
        BTSelector sequence_team = new BTSelector();

        #region Leader Branch

        BTSequence sequence = new BTSequence();

        BTIsLeader isLeader = new BTIsLeader(squadMember);
        BTThereIs thereIs = new BTThereIs(targetController, target);


        BTParallelSelector parallelSelector_1 = new BTParallelSelector();

        BTSee see = new BTSee(targetController, target, distanceToTarget);
        BTMoveByNavMesh moveTo = new BTMoveByNavMesh(navMeshController, targetController, attributes.speed, distanceToTarget);

        parallelSelector_1.SetNode(see);
        parallelSelector_1.SetNode(moveTo);


        sequence.SetNode(isLeader);
        sequence.SetNode(thereIs);
        sequence.SetNode(parallelSelector_1);

        #endregion

        #region Member Branch
        BTSequence sequence_2 = new BTSequence();

        BTHasLeader hasLeader = new BTHasLeader(squadMember);
        BTUpdateLeader updateLeader = new BTUpdateLeader(squadMember, targetController);
        sequence_2.SetNode(hasLeader);
        sequence_2.SetNode(updateLeader);


        BTParallelSelector parallelSelector_2 = new BTParallelSelector();

        sequence_2.SetNode(parallelSelector_2);

        BTSee see_2 = new BTSee(targetController, target, distanceToTarget, true);
        BTMoveByNavMesh moveLeader = new BTMoveByNavMesh(navMeshController, targetController, attributes.speed, distanceToLeader);

        parallelSelector_2.SetNode(see_2);
        parallelSelector_2.SetNode(moveLeader);

        sequence_2.SetNode(parallelSelector_2);
        #endregion


        #region  Apply Node
        sequence_team.SetNode(sequence);

        sequence_team.SetNode(sequence_2);

        #endregion

        return sequence_team;
    }
    #endregion

}
