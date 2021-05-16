using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNextToTarget : BTNode
{

    private TargetController targetController;

    private float distance;


    public BTNextToTarget(TargetController _targetController, float _distance)
    {
        targetController = _targetController;

        distance = _distance;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {

        status = Status.FAILURE;

        float currentDistance = Vector3.Distance(bt.transform.position, targetController.GetTarget().position);

        if (currentDistance < distance)
        {
            status = Status.SUCCESS;
        }


        yield break;
    }
}
