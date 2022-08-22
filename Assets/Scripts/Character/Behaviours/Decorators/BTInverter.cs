using System.Collections;

namespace ZombieDiorama.Character.Behaviours.Decorators
{
    public class BTInverter : BTNode
    {
        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;

            if (children.Count == 0)
            {
                CurrentStatus = Status.FAILURE;
                yield break;
            }

            BTNode node = children[0];

            yield return bt.StartCoroutine(node.Run(bt));

            if (node.CurrentStatus.Equals(Status.SUCCESS))
            {
                CurrentStatus = Status.FAILURE;
                yield break;
            }
            else if (node.CurrentStatus.Equals(Status.FAILURE))
            {
                CurrentStatus = Status.SUCCESS;
                yield break;
            }

            CurrentStatus = Status.FAILURE;
        }
    }
}