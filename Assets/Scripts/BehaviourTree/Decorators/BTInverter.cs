using System.Collections;
using UnityEngine;

public class BTInverter : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {

        status = Status.RUNNING;

        if (children.Count == 0)
        {
            status = Status.FAILURE;
            yield break;
        }

        BTNode node = children[0];

        yield return bt.StartCoroutine(node.Run(bt));

        if (node.status.Equals(Status.SUCCESS))
        {
            status = Status.FAILURE;
            yield break;
        }
        else if (node.status.Equals(Status.FAILURE))
        {
            status = Status.SUCCESS;
            yield break;
        }

        status = Status.FAILURE;

    }
}