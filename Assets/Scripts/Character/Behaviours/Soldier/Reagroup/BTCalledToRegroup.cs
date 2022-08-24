using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Character.Handler.Regroup;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTCalledToRegroup : BTNode
    {
        private int currentRegroupId = -1;
        private TargetHandler targetHandler;
        private float distance;

        public BTCalledToRegroup(TargetHandler _targetHandler, float _distance)
        {
            targetHandler = _targetHandler;
            distance = _distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!RegroupHandler.Instance) yield break;

            bool canRegroup = RegroupHandler.Instance.CanRegroup(currentRegroupId);

            if (canRegroup)
            {
                CurrentStatus = Status.SUCCESS;

                if (targetHandler.GetTarget())
                {
                    float currentDistance = Vector3.Distance(bt.transform.position, targetHandler.GetTarget().position);

                    if (currentDistance < distance)
                    {
                        currentRegroupId = RegroupHandler.Instance.GetRegroupId();
                    }
                }
            }
            yield break;
        }
    }
}