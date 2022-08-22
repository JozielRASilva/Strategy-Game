using System.Collections;

namespace ZombieDiorama.Character.Behaviours
{
    public class BTSequence : BTNode
    {
        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.RUNNING;

            foreach (var node in children)
            {
                yield return bt.StartCoroutine(node.Run(bt));

                if (node.status.Equals(Status.FAILURE))
                {
                    status = Status.FAILURE;
                    break;
                }
            }

            if (status.Equals(Status.RUNNING))
            {
                status = Status.SUCCESS;
            }
        }
    }
}