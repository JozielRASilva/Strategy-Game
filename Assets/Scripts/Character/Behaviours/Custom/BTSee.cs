using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTSee : BTNode
    {
        private string target;
        private float rangeToCheckEnemy = 5;
        private TargetHandler targetHandler;
        private bool cleanTarget = false;

        public BTSee(TargetHandler _targetHandler, string _target, float _rangeToCheckEnemy)
        {
            target = _target;
            rangeToCheckEnemy = _rangeToCheckEnemy;
            targetHandler = _targetHandler;
        }

        public BTSee(TargetHandler _targetHandler, string _target, float _rangeToCheckEnemy, bool _cleanTarget)
        {
            target = _target;
            rangeToCheckEnemy = _rangeToCheckEnemy;
            targetHandler = _targetHandler;
            cleanTarget = _cleanTarget;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;
            List<GameObject> enemies = TagObjectsCacher.GetObjects(target);
            List<GameObject> enemiesThatCanSee = new List<GameObject>();

            foreach (var enemy in enemies)
            {
                if (enemy == bt.gameObject) continue;
                float distance = Vector3.Distance(enemy.transform.position, bt.transform.position);

                if (distance < rangeToCheckEnemy)
                {
                    enemiesThatCanSee.Add(enemy);
                    CurrentStatus = Status.SUCCESS;
                }
            }

            if (CurrentStatus.Equals(Status.SUCCESS))
            {
                if (targetHandler)
                    if (!cleanTarget)
                    {
                        Transform selectedTarget = GetTarget(bt.transform, enemiesThatCanSee);
                        if (selectedTarget)
                            targetHandler.SetTarget(selectedTarget);
                    }
                    else
                    {
                        targetHandler.SetTarget(null);
                    }
            }

            if (CurrentStatus.Equals(Status.RUNNING))
                CurrentStatus = Status.FAILURE;

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