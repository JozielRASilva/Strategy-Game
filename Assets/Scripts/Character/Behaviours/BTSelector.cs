using System.Collections;

namespace ZombieDiorama.Character.Behaviours
{
public class BTSelector : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        CurrentStatus = Status.RUNNING;

        foreach (var node in Children)
        {
            yield return bt.StartCoroutine(node.Run(bt));

            if (node.CurrentStatus.Equals(Status.SUCCESS))
            {
                CurrentStatus = Status.SUCCESS;
                break;
            }
        }

        if (CurrentStatus.Equals(Status.RUNNING))
        {
            CurrentStatus = Status.FAILURE;
        }
    }
}
}