using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.AIs.Controllers;
using ZombieDiorama.Character.AIs.Controllers.Regroup;

namespace ZombieDiorama.Character.AIs.Behaviours.Custom.Soldier.Reagroup
{
    public class BTUpdateRegroup : BTNode
    {
        private TargetController targetController;

        public BTUpdateRegroup(TargetController _targetController)
        {
            targetController = _targetController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (!RegroupController.Instance)
                yield break;

            Transform regroupPoint = RegroupController.Instance.GetRegroupPoint();

            if (regroupPoint != null)
            {
                targetController.SetTarget(regroupPoint);

                status = Status.SUCCESS;
            }
            yield break;
        }
    }
}
