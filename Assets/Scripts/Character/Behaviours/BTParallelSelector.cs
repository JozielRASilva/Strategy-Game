using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace ZombieDiorama.Character.Behaviours
{
    public class BTParallelSelector : BTNode
    {
        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;

            Dictionary<BTNode, Coroutine> coroutines = new Dictionary<BTNode, Coroutine>();
            foreach (var node in children)
            {
                coroutines.Add(node, bt.StartCoroutine(node.Run(bt)));
            }

            while (CurrentStatus.Equals(Status.RUNNING))
            {
                CurrentStatus = Status.FAILURE;

                foreach (var node in children)
                {
                    if (node.CurrentStatus.Equals(Status.RUNNING)) CurrentStatus = Status.RUNNING;

                    if (node.CurrentStatus.Equals(Status.SUCCESS))
                    {
                        CurrentStatus = Status.SUCCESS;
                        break;
                    }
                }

                if (CurrentStatus.Equals(Status.RUNNING))
                {
                    foreach (var node in children)
                    {
                        if (node.CurrentStatus.Equals(Status.FAILURE))
                        {
                            coroutines[node] = bt.StartCoroutine(node.Run(bt));
                        }
                    }
                }
                else if (CurrentStatus.Equals(Status.SUCCESS))
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