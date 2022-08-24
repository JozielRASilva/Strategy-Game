using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTCheckWaypoint : BTNode
    {
        private float distance = 1;
        private TargetHandler targetHandler;

        public BTCheckWaypoint(float _distance, TargetHandler _targetHandler)
        {
            distance = _distance;
            targetHandler = _targetHandler;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            Transform zombie = bt.transform;
            Transform target = targetHandler.GetTarget();

            if (target)
            {
                if (Vector3.Distance(zombie.position, target.position) < distance)
                {
                    CurrentStatus = Status.SUCCESS;
                }
                else CurrentStatus = Status.FAILURE;
            }
            else CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}