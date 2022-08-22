using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTCloseToTarget : BTNode
    {
        private TargetController _targetZombie;
        private float _minDistance;
        private float _maxDistance;

        public BTCloseToTarget(TargetController targetZombie, float minDistance, float maxDistance)
        {
            _targetZombie = targetZombie;
            _minDistance = minDistance;
            _maxDistance = maxDistance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (_targetZombie)
            {
                if (_targetZombie.GetTarget())
                    yield break;

                float distance = Vector3.Distance(bt.transform.position, _targetZombie.GetTarget().position);

                if (distance > _maxDistance)
                {
                    _targetZombie.SetTarget(null);
                    CurrentStatus = Status.SUCCESS;
                    yield break;
                }

                if (distance < _minDistance)
                {
                    _targetZombie.SetTarget(null);
                    CurrentStatus = Status.SUCCESS;
                    yield break;
                }
            }
            yield break;
        }
    }
}