using System.Collections;
using UnityEngine;

public class BTCombat : BTNode
{

    private string enemy;
    private float coolDown;
    private GameObject prefab;

    public BTCombat(string _target, float _coolDown, GameObject projectile)
    {
        enemy = _target;
        coolDown = _coolDown;
        prefab = projectile;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        GameObject selectedEnemy = GetTarget(bt.transform);


        if (selectedEnemy) yield return new WaitForSeconds(coolDown);

        if (selectedEnemy)
        {
            bt.transform.LookAt(selectedEnemy.transform);

            Vector3 position = bt.transform.position + bt.transform.forward;

            GameObject shoot = GameObject.Instantiate(prefab, position, Quaternion.identity);
            shoot.GetComponent<Rigidbody>().AddForce(bt.transform.forward * 100);
            GameObject.Destroy(shoot, 5);

            status = Status.SUCCESS;
        }
        else
        {
            status = Status.FAILURE;
        }

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