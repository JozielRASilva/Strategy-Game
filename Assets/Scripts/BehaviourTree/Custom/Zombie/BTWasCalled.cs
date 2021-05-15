using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWasCalled : BTNode
{
    bool calling;
    TargetController target;

    public BTWasCalled (TargetController _target, bool _calling)
    {
        target = _target;
        calling = _calling;

    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (calling)
        {
           //GameObject[] zombie = GameObject.FindGameObjectsWithTag("CallCounter");

            GameObject[] calls = GameObject.FindGameObjectsWithTag("CallCounter");
            foreach (GameObject call in calls)
            {
                if (bt.gameObject == call) continue;
                
                    target.SetTarget(call.transform);

                    status = Status.SUCCESS;
                    break;
            }

            status = Status.SUCCESS;
        }
        
        yield break;
    }
}
