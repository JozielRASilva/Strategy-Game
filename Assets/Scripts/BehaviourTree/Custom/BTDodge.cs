using System.Collections;
using UnityEngine;

public class BTDodge : BTNode
{


    private string enemy;
    private Vector2 dodgeTimeRange = new Vector2();

    public BTDodge(string target, Vector2 _dodgeTimeRange)
    {
        enemy = target;
        dodgeTimeRange = _dodgeTimeRange;
    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        GameObject selectedEnemy = GetTarget(bt.transform);

        if (selectedEnemy)
        {
            float time = Random.Range(dodgeTimeRange.x, dodgeTimeRange.y);
            float signal = Mathf.Sign(Random.Range(-1f, 1));
            while (time > 0)
            {
                time -= Time.deltaTime;

                if (selectedEnemy == null)
                {
                    status = Status.FAILURE;
                    break;
                }

                bt.transform.LookAt(selectedEnemy.transform);
                bt.transform.Translate(Vector3.right * signal * Time.deltaTime);
                yield return null;
            }
        }
        else status = Status.FAILURE;


    }


    public GameObject GetTarget(Transform current)
    {
        GameObject selected = null;

        GameObject[] targets = GameObject.FindGameObjectsWithTag(enemy);

        float lastDistance = 0;

        foreach (var _target in targets)
        {
            if (_target == current.gameObject) continue;
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

        if (selected) return selected;
        else return null;
    }
}