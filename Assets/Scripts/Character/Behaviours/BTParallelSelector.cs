using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace ZombieDiorama.Character.Behaviours
{
    public class BTParallelSelector : BTNode
    {
        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.RUNNING;

            Dictionary<BTNode, Coroutine> coroutines = new Dictionary<BTNode, Coroutine>();
            foreach (var node in children)
            {
                coroutines.Add(node, bt.StartCoroutine(node.Run(bt)));
            }

            while (status.Equals(Status.RUNNING))
            {
                status = Status.FAILURE;

                foreach (var node in children)
                {
                    if (node.status.Equals(Status.RUNNING)) status = Status.RUNNING;

                    if (node.status.Equals(Status.SUCCESS))
                    {
                        status = Status.SUCCESS;
                        break;
                    }
                }

                if (status.Equals(Status.RUNNING))
                {
                    foreach (var node in children)
                    {
                        if (node.status.Equals(Status.FAILURE))
                        {
                            coroutines[node] = bt.StartCoroutine(node.Run(bt));
                        }
                    }
                }
                else if (status.Equals(Status.SUCCESS))
                {
                    foreach (var pair in coroutines)
                    {
                        if (pair.Value != null) bt.StopCoroutine(pair.Value);
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}