using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AISupportSoldier : MonoBehaviour
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

        if (!attributes) return;

        BTSequence root = new BTSequence();

        // SET OBJECT
        BTSequence sequence_setObject = new BTSequence();

        BTObjectToSet thereIsObjectToSet = new BTObjectToSet();
        
        BTInverter inverter = new BTInverter();
        BTObjectSetted objectSetted = new BTObjectSetted();
        inverter.SetNode(objectSetted);

        BTUpdateObjectToSet updateObjectToSet = new BTUpdateObjectToSet();


        BTSequence sequence_1 = new BTSequence();
        BTParallelSelector parallelSelector_1 = new BTParallelSelector();

        BTNextToTarget nextToTarget = new BTNextToTarget();
        BTMoveByNavMesh moveToSet = new BTMoveByNavMesh(navMeshController, targetController, attributes.speed, 0.5f);

        BTSetObject setObject = new BTSetObject();
        

        sequence_setObject.SetNode(thereIsObjectToSet);
        sequence_setObject.SetNode(inverter);
        sequence_setObject.SetNode(updateObjectToSet);
        sequence_setObject.SetNode(sequence_1);



            // Selector paralelo
                // Proximo de onde colocar?
                // Mover para colocar
            // Colocar


        behaviourTree.Build(root);

    }
}
