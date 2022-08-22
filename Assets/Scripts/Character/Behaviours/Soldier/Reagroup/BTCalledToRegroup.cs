using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Controllers.Regroup;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTCalledToRegroup : BTNode
    {
        private int _currentRegroupId = -1;
        private TargetController _targetController;
        private float _distance;

        public BTCalledToRegroup(TargetController targetController, float distance)
        {
            _targetController = targetController;
            _distance = distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!RegroupController.Instance) yield break;

            bool canRegroup = RegroupController.Instance.CanRegroup(_currentRegroupId);

            if (canRegroup)
            {
                CurrentStatus = Status.SUCCESS;

                if (_targetController.GetTarget())
                {
                    float currentDistance = Vector3.Distance(bt.transform.position, _targetController.GetTarget().position);

                    if (currentDistance < _distance)
                    {
                        _currentRegroupId = RegroupController.Instance.GetRegroupId();
                    }
                }
            }
            yield break;
        }
    }
}