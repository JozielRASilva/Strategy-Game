using System.Collections;
using UnityEngine;

namespace ZombieDiorama.Character.AIs.Behaviours
{
public class BTSelector : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        foreach (var node in children)
        {
            yield return bt.StartCoroutine(node.Run(bt));

            if (node.status.Equals(Status.SUCCESS))
            {
                status = Status.SUCCESS;
                break;
            }
        }

        if (status.Equals(Status.RUNNING))
        {
            status = Status.FAILURE;
        }
    }
}
}