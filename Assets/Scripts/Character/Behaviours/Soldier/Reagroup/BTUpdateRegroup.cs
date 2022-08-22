using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Controllers.Regroup;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTUpdateRegroup : BTNode
    {
        private TargetController _targetController;

        public BTUpdateRegroup(TargetController targetController)
        {
            _targetController = targetController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!RegroupController.Instance)
                yield break;

            Transform regroupPoint = RegroupController.Instance.GetRegroupPoint();

            if (regroupPoint != null)
            {
                _targetController.SetTarget(regroupPoint);

                CurrentStatus = Status.SUCCESS;
            }
            yield break;
        }
    }
}
