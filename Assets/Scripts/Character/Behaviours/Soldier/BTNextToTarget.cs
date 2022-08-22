using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTNextToTarget : BTNode
    {
        private TargetController _targetController;
        private float _distance;

        public BTNextToTarget(TargetController targetController, float distance)
        {
            _targetController = targetController;
            _distance = distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            Transform target = _targetController.GetTarget();

            if (target)
            {
                float currentDistance = Vector3.Distance(bt.transform.position, target.position);
                if (currentDistance < _distance)
                {
                    CurrentStatus = Status.SUCCESS;
                }
            }
            yield break;
        }
    }
}