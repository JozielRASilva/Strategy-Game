using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCloseToTarget : BTNode
{
    private TargetController targetZombie;

    private float minDistance;
    private float maxDistance;

    public BTCloseToTarget(TargetController _targetZombie, float _minDistance, float _maxDistance)
    {
        targetZombie = _targetZombie;
        minDistance = _minDistance;
        maxDistance = _maxDistance;

    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        Print();
        if (targetZombie)
        {
            float distance = Vector3.Distance(bt.transform.position, targetZombie.GetTarget().position);

            //Print(distance.ToString());

            if (distance > maxDistance)
            {
                targetZombie.SetTarget(null);

                status = Status.SUCCESS;

                yield break;
            }

            if (distance < minDistance)
            {
                targetZombie.SetTarget(null);

                status = Status.SUCCESS;
                yield break;
            }
        }

        yield break;
    }
}
