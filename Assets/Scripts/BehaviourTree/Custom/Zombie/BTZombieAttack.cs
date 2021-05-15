using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTZombieAttack : BTNode
{
    GameObject hitboxes;
    float coolDown;

    public BTZombieAttack (GameObject _hitboxes, float _coolDown)
    {
        hitboxes = _hitboxes;
        coolDown = _coolDown;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        yield return new WaitForSeconds(coolDown);

        hitboxes.SetActive(true);

        yield return new WaitForSeconds(coolDown);

        hitboxes.SetActive(false);
        status = Status.SUCCESS;

        yield break;
    }
}
