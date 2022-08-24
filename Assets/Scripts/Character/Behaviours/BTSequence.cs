using System.Collections;

namespace ZombieDiorama.Character.Behaviours
{
    public class BTSequence : BTNode
    {
        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;

            foreach (var node in Children)
            {
                yield return bt.StartCoroutine(node.Run(bt));

                if (node.CurrentStatus.Equals(Status.FAILURE))
                {
                    CurrentStatus = Status.FAILURE;
                    break;
                }
            }

            if (CurrentStatus.Equals(Status.RUNNING))
            {
                CurrentStatus = Status.SUCCESS;
            }
        }
    }
}