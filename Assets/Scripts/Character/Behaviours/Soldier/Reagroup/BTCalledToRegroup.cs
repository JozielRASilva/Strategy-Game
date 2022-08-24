using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Character.Handler.Regroup;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTCalledToRegroup : BTNode
    {
        private int currentRegroupId = -1;
        private TargetHandler targetController;
        private float distance;

        public BTCalledToRegroup(TargetHandler _targetController, float _distance)
        {
            targetController = _targetController;
            distance = _distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (!RegroupHandler.Instance) yield break;

            bool canRegroup = RegroupHandler.Instance.CanRegroup(currentRegroupId);

            if (canRegroup)
            {
                status = Status.SUCCESS;

                if (targetController.GetTarget())
                {
                    float currentDistance = Vector3.Distance(bt.transform.position, targetController.GetTarget().position);

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