using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTObjectSetted : BTNode
{
    public BTObjectSetted()
    {

    }

    public override IEnumerator Run(BehaviourTree bt)
    {

        status = Status.FAILURE;

        if (!ObjectSetterManager.Instance)
            yield break;

        ObjectToSet objectToSet = ObjectSetterManager.Instance.GetObjectToSet(bt.gameObject);

        if (objectToSet != null)
        {
            status = Status.SUCCESS;
        }

        yield break;
    }
}
