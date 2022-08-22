using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Controllers.Regroup;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTCalledToRegroup : BTNode
    {
        private int currentRegroupId = -1;
        private TargetController targetController;
        private float distance;

        public BTCalledToRegroup(TargetController _targetController, float _distance)
        {
            targetController = _targetController;
            distance = _distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (!RegroupController.Instance) yield break;

            bool canRegroup = RegroupController.Instance.CanRegroup(currentRegroupId);

            if (canRegroup)
            {
                status = Status.SUCCESS;

                if (targetController.GetTarget())
                {
                    float currentDistance = Vector3.Distance(bt.transform.position, targetController.GetTarget().position);

                    if (currentDistance < distance)
                    {
                        currentRegroupId = RegroupController.Instance.GetRegroupId();
                    }
                }
            }
            yield break;
        }
    }
}