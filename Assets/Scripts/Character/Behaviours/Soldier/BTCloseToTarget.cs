using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTCloseToTarget : BTNode
    {
        private TargetHandler targetZombie;
        private float minDistance;
        private float maxDistance;

        public BTCloseToTarget(TargetHandler _targetZombie, float _minDistance, float _maxDistance)
        {
            targetZombie = _targetZombie;
            minDistance = _minDistance;
            maxDistance = _maxDistance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (targetZombie)
            {
                if (targetZombie.GetTarget())
                    yield break;

                float distance = Vector3.Distance(bt.transform.position, targetZombie.GetTarget().position);

                if (distance > maxDistance)
                {
                    targetZombie.SetTarget(null);
                    CurrentStatus = Status.SUCCESS;
                    yield break;
                }

                if (distance < minDistance)
                {
                    targetZombie.SetTarget(null);
                    CurrentStatus = Status.SUCCESS;
                    yield break;
                }
            }
            yield break;
        }
    }
}