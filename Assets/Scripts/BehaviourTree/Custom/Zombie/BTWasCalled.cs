using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWasCalled : BTNode
{
    private TargetController target;
    private bool calling;

    public BTWasCalled (TargetController _target, bool _calling)
    {
        target = _target;
        calling = _calling;
    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        GameObject[] calls = GameObject.FindGameObjectsWithTag("CallCounter");
        int i = 0;

        Debug.Log("entrou");
        foreach (GameObject call in calls)
        {
            if (bt.gameObject == call) continue;

            // verificar a distancia q nem o SeeSoldier
            if (target)
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
