using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTSee : BTNode
    {
        public string Target;
        public float RangeToCheckEnemy = 5;

        private TargetController _targetController;
        private bool _cleanTarget = false;

        public BTSee(TargetController _targetController, string _target, float _rangeToCheckEnemy)
        {
            Target = _target;
            RangeToCheckEnemy = _rangeToCheckEnemy;
            this._targetController = _targetController;
        }

        public BTSee(TargetController _targetController, string _target, float _rangeToCheckEnemy, bool _cleanTarget)
        {
            Target = _target;
            RangeToCheckEnemy = _rangeToCheckEnemy;
            this._targetController = _targetController;
            this._cleanTarget = _cleanTarget;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.RUNNING;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Target);
            List<GameObject> enemiesThatCanSee = new List<GameObject>();

            foreach (var enemy in enemies)
            {
                if (enemy == bt.gameObject) continue;
                float distance = Vector3.Distance(enemy.transform.position, bt.transform.position);

                if (distance < RangeToCheckEnemy)
                {
                    enemiesThatCanSee.Add(enemy);
                    status = Status.SUCCESS;
                }
            }

            if (status.Equals(Status.SUCCESS))
            {
                if (_targetController)
                    if (!_cleanTarget)
                    {
                        Transform selectedTarget = GetTarget(bt.transform, enemiesThatCanSee);
                        if (selectedTarget)
                            _targetController.SetTarget(selectedTarget);
                    }
                    else
                    {
                        _targetController.SetTarget(null);
                    }
            }

            if (status.Equals(Status.RUNNING))
                status = Status.FAILURE;

            yield break;
        }

        public Transform GetTarget(Transform current, List<GameObject> targets)
        {
            GameObject selected = null;
            float lastDistance = 0;
            foreach (var _target in targets)
            {
                float distance = Vector3.Distance(current.position, _target.transform.position);
                if (!selected)
                {
                    selected = _target;
                    lastDistance = distance;
                }
                else
                {
                    if (distance < lastDistance)
                    {
                        selected = _target;
                        lastDistance = distance;
                    }
                }
            }
            if (selected) return selected.transform;
            else return null;
        }
    }
}