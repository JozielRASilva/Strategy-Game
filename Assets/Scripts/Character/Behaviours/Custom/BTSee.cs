using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTSee : BTNode
    {
        private TargetHandler targetController;
        public string target;
        public float rangeToCheckEnemy = 5;
        private bool cleanTarget = false;

        public BTSee(TargetHandler _targetController, string _target, float _rangeToCheckEnemy)
        {
            target = _target;
            rangeToCheckEnemy = _rangeToCheckEnemy;
            targetController = _targetController;
        }

        public BTSee(TargetHandler _targetController, string _target, float _rangeToCheckEnemy, bool _cleanTarget)
        {
            target = _target;
            rangeToCheckEnemy = _rangeToCheckEnemy;
            targetController = _targetController;
            cleanTarget = _cleanTarget;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.RUNNING;
            List<GameObject> enemies = TagObjectsCacher.GetObjects(target);
            List<GameObject> enemiesThatCanSee = new List<GameObject>();

            foreach (var enemy in enemies)
            {
                if (enemy == bt.gameObject) continue;
                float distance = Vector3.Distance(enemy.transform.position, bt.transform.position);

                if (distance < rangeToCheckEnemy)
                {
                    enemiesThatCanSee.Add(enemy);
                    status = Status.SUCCESS;
                }
            }

            if (status.Equals(Status.SUCCESS))
            {
                if (targetController)
                    if (!cleanTarget)
                    {
                        Transform selectedTarget = GetTarget(bt.transform, enemiesThatCanSee);
                        if (selectedTarget)
                            targetController.SetTarget(selectedTarget);
                    }
                    else
                    {
                        targetController.SetTarget(null);
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