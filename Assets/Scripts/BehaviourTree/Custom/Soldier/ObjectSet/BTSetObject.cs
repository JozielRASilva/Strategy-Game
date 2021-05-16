using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetObject : BTNode
{

    private float delayToSet;

    public BTSetObject(float _delayToSet)
    {
        delayToSet = _delayToSet;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        if (!ObjectSetterManager.Instance)
            yield break;

        ObjectToSet objectToSet = ObjectSetterManager.Instance.GetObjectToSet(bt.gameObject);

        if (objectToSet != null)
        {

            yield return new WaitForSeconds(delayToSet);

            ObjectSetterManager.Instance.SetObject(objectToSet);

            status = Status.SUCCESS;
        }
        else
        {
            status = Status.SUCCESS;
        }

        yield break;
    }
}
