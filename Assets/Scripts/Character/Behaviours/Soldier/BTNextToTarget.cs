using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTNextToTarget : BTNode
    {
        private TargetHandler targetController;
        private float distance;

        public BTNextToTarget(TargetHandler _targetController, float _distance)
        {
            targetController = _targetController;
            distance = _distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            Transform target = targetController.GetTarget();

            if (target)
            {
                float currentDistance = Vector3.Distance(bt.transform.position, target.position);
                if (currentDistance < distance)
                {
                    status = Status.SUCCESS;
                }
            }
            yield break;
        }
    }
}