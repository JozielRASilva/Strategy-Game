using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Character.Handler.Regroup;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTUpdateRegroup : BTNode
    {
        private TargetHandler targetController;

        public BTUpdateRegroup(TargetHandler _targetController)
        {
            targetController = _targetController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (!RegroupHandler.Instance)
                yield break;

            Transform regroupPoint = RegroupHandler.Instance.GetRegroupPoint();

            if (regroupPoint != null)
            {
                targetController.SetTarget(regroupPoint);

                status = Status.SUCCESS;
            }
            yield break;
        }
    }
}
