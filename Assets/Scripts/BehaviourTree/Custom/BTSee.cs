using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BTSee : BTNode
{
    private TargetController targetController;
    public string target;
    public float rangeToCheckEnemy = 5;

    public BTSee(TargetController _targetController, string _target, float _rangeToCheckEnemy)
    {
        target = _target;
        rangeToCheckEnemy = _rangeToCheckEnemy;
        targetController = _targetController;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(target);

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
            Transform selectedTarget = GetTarget(bt.transform, enemiesThatCanSee);

            if (selectedTarget)
                targetController.SetTarget(selectedTarget);
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