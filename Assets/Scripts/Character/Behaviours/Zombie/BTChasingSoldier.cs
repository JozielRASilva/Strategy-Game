using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTChasingSoldier : BTNode
    {
        private TargetController _targetSoldier;
        private float _minDistance;
        private float _maxDistance;

        public BTChasingSoldier(TargetController targetSoldier, float minDistance, float maxDistance)
        {
            _targetSoldier = targetSoldier;
            _minDistance = minDistance;
            _maxDistance = maxDistance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (_targetSoldier)
            {
                float distance = Vector3.Distance(bt.transform.position, _targetSoldier.GetTarget().position);

                if (distance > _maxDistance)
                {
                    _targetSoldier.SetTarget(null);
                    CurrentStatus = Status.SUCCESS;
                    yield break;
                }

                if (distance < _minDistance)
                {
                    _targetSoldier.SetTarget(null);
                    CurrentStatus = Status.SUCCESS;
                    yield break;
                }
            }
            yield break;
        }
    }
}