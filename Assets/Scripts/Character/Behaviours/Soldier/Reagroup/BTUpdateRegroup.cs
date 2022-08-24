using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Character.Handler.Regroup;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTUpdateRegroup : BTNode
    {
        private TargetHandler targetHandler;

        public BTUpdateRegroup(TargetHandler _targetHandler)
        {
            targetHandler = _targetHandler;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (!RegroupHandler.Instance)
                yield break;

            Transform regroupPoint = RegroupHandler.Instance.GetRegroupPoint();

            if (regroupPoint != null)
            {
                targetHandler.SetTarget(regroupPoint);

                status = Status.SUCCESS;
            }
            yield break;
        }
    }
}
