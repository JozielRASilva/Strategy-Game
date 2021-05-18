using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BTHitbox : BTNode
{
    private GameObject hitboxes;
    private float coolDown;
    private float rest;
    private TargetController targetController;

    public BTHitbox(GameObject _hitboxes, float _coolDown, float _rest, TargetController _targetController)
    {
        hitboxes = _hitboxes;
        coolDown = _coolDown;
        targetController = _targetController;

        rest = _rest;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        yield return new WaitForSeconds(coolDown);

        hitboxes.SetActive(true);

        yield return new WaitForSeconds(rest);

        hitboxes.SetActive(false);
        status = Status.SUCCESS;

        yield break;
    }
}
