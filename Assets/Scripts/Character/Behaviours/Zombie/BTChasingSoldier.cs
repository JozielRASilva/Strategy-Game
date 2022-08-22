using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTChasingSoldier : BTNode
    {
        private TargetController targetSoldier;
        private float minDistance;
        private float maxDistance;

        public BTChasingSoldier(TargetController _targetSoldier, float _minDistance, float _maxDistance)
        {
            targetSoldier = _targetSoldier;
            minDistance = _minDistance;
            maxDistance = _maxDistance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (targetSoldier)
            {
                float distance = Vector3.Distance(bt.transform.position, targetSoldier.GetTarget().position);

                if (distance > maxDistance)
                {
                    targetSoldier.SetTarget(null);
                    status = Status.SUCCESS;
                    yield break;
                }

                if (distance < minDistance)
                {
                    targetSoldier.SetTarget(null);
                    status = Status.SUCCESS;
                    yield break;
                }
            }
            yield break;
        }
    }
}