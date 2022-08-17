using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.AIs.Controllers;

namespace ZombieDiorama.Character.AIs.Behaviours.Custom.Zombie
{
    public class BTCheckWaypoint : BTNode
    {
        private float distance = 1;
        private TargetController targetController;

        public BTCheckWaypoint(float _distance, TargetController _targetController)
        {
            distance = _distance;
            targetController = _targetController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            Transform zombie = bt.transform;
            Transform target = targetController.GetTarget();

            if (target)
            {
                if (Vector3.Distance(zombie.position, target.position) < distance)
                {
                    status = Status.SUCCESS;
                }
                else status = Status.FAILURE;
            }
            else status = Status.SUCCESS;

            yield break;
        }
    }
}