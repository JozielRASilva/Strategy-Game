using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTChasingSoldier : BTNode
{
    private TargetController targetSoldier;

    private float minDistance;
    private float maxDistance;

    public BTChasingSoldier (TargetController _targetSoldier, float _minDistance, float _maxDistance)
    {
        targetSoldier = _targetSoldier;
        minDistance = _minDistance;
        maxDistance = _maxDistance;

}

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        Print();
        if (targetSoldier)
        {
            float distance = Vector3.Distance(bt.transform.position, targetSoldier.GetTarget().position);

            //Print(distance.ToString());

            if (distance > maxDistance)
            {
                targetSoldier.SetTarget(null);

                status = Status.SUCCESS;

                yield break;
            }

            if (distance < minDistance)
            {
                targetSoldier.SetTarget(null);

                status = Status.SUCCESS;
                yield break;
            }
        }

        yield break;
    }
}