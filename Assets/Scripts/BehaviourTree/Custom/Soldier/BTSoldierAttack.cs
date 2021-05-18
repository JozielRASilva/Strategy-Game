using System.Collections;
using UnityEngine;

public class BTSoldierAttack : BTNode
{

    private TargetController targetZombie;
    private float coolDown;
    private GameObject prefab;
    private GameObject muzzle;

    public BTSoldierAttack(TargetController _targetZombie, float _coolDown, GameObject projectile, GameObject _muzzle)
    {
        targetZombie = _targetZombie;
        coolDown = _coolDown;
        prefab = projectile;
        muzzle = _muzzle;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        GameObject selectedEnemy = GetTarget(bt.transform);


        if (selectedEnemy) yield return new WaitForSeconds(coolDown);

        if (selectedEnemy)
        {
            Vector3 lookAtPosition =
            new Vector3(selectedEnemy.transform.position.x, bt.transform.position.y, selectedEnemy.transform.position.z);

            bt.transform.LookAt(lookAtPosition);

            Vector3 position = muzzle.transform.position;

            GameObject shoot = GameObject.Instantiate(prefab, position, Quaternion.identity);
            shoot.GetComponent<Rigidbody>().AddForce(bt.transform.forward * 200);
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

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Zombie");

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