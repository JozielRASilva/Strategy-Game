using UnityEngine;
using System.Collections;

public class BTThereIs : BTNode
{

    public string target;
    private TargetController targetController;

    public BTThereIs(TargetController _targetController, string _target)
    {
        target = _target;
        targetController = _targetController;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        if (GameObject.FindGameObjectWithTag(target))
        {

            Transform target = GetTarget(bt.transform);

            if (target)
            {
                targetController.SetTarget(target);
            }

            status = Status.SUCCESS;

        }
        else
        {
            status = Status.FAILURE;
        }

        yield break;
    }


    public Transform GetTarget(Transform current)
    {
        GameObject selected = null;

        GameObject[] targets = GameObject.FindGameObjectsWithTag(target);

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