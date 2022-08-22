using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTCheckWaypoint : BTNode
    {
        private float _distance = 1;
        private TargetController _targetController;

        public BTCheckWaypoint(float distance, TargetController targetController)
        {
            _distance = distance;
            _targetController = targetController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            Transform zombie = bt.transform;
            Transform target = _targetController.GetTarget();

            if (target)
            {
                if (Vector3.Distance(zombie.position, target.position) < _distance)
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