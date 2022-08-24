using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTNextToTarget : BTNode
    {
        private TargetHandler targetHandler;
        private float distance;

        public BTNextToTarget(TargetHandler _targetHandler, float _distance)
        {
            targetHandler = _targetHandler;
            distance = _distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            Transform target = targetHandler.GetTarget();

            if (target)
            {
                float currentDistance = Vector3.Distance(bt.transform.position, target.position);
                if (currentDistance < distance)
                {
                    CurrentStatus = Status.SUCCESS;
                }
            }
            yield break;
        }
    }
}