using System.Collections;
using UnityEngine;

public class BTSoldierAttack : BTNode
{

    private TargetController targetZombie;
    private float coolDown;
    private GameObject prefab;
    private GameObject muzzle;

    private float damping;
    private string targetTag;

    public BTSoldierAttack(TargetController _targetZombie, float _coolDown, GameObject projectile, GameObject _muzzle, float _damping, string _targetTag)
    {
        targetZombie = _targetZombie;
        coolDown = _coolDown;
        prefab = projectile;
        muzzle = _muzzle;
        targetTag = _targetTag;
        damping = _damping;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        GameObject selectedEnemy = GetTarget(bt.transform);

        if (selectedEnemy)
        {

            float timeStamp = Time.time + coolDown;

            while (timeStamp > Time.time)
            {
                var lookPos = selectedEnemy.transform.position - bt.transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, rotation, Time.deltaTime * damping);

                yield return null;
            }


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

        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

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