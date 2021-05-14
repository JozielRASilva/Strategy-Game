using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTChasingSoldier : BTNode
{
    TargetController target;

    Transform soldierPosition;

    public BTChasingSoldier (TargetController _target, Transform _soldierPosition)
    {
        target = _target;
        soldierPosition = _soldierPosition;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;


        target.SetTarget(soldierPosition);


        status = Status.SUCCESS;


        yield break;
    }
}
