using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWasCalled : BTNode
{
    private TargetController target;

    private float distanceCall;

    public BTWasCalled (TargetController _target, float _distanceCall)
    {
        target = _target;
        distanceCall = _distanceCall;
    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        GameObject[] calls = GameObject.FindGameObjectsWithTag("CallCounter");
        int i = 0;

        foreach (GameObject call in calls)
        {
            if (bt.gameObject == call) continue;

            if (Vector3.Distance(bt.transform.position, call.transform.position) < distanceCall)
            {
                    target.SetTarget(call.transform);
                    i++;

                    if (i == 8)
                    {
                        target.SetTarget(null);
                        i = 0;
                    }

                    status = Status.SUCCESS;
                    yield break;
            }


        }

        yield break;
    }
}
