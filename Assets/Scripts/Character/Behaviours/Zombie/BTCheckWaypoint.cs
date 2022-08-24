using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTCheckWaypoint : BTNode
    {
        private float distance = 1;
        private TargetHandler targetController;

        public BTCheckWaypoint(float _distance, TargetHandler _targetController)
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