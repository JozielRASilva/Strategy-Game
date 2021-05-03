using System.Collections;
using UnityEngine;

public class BTSee : BTNode
{

    public string target;
    public float rangeToCheckEnemy = 5;

    public BTSee(string _target, float _rangeToCheckEnemy)
    {
        target = _target;
        rangeToCheckEnemy = _rangeToCheckEnemy;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(target);
        foreach (var enemy in enemies)
        {
            if (enemy == bt.gameObject) continue;
            float distance = Vector3.Distance(enemy.transform.position, bt.transform.position);

            if (distance < rangeToCheckEnemy)
            {
                status = Status.SUCCESS;
            }

        }

        if (status.Equals(Status.RUNNING))
            status = Status.FAILURE;


        yield break;
    }

}