using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTObjectToSet : BTNode
{

    SquadMember member;
    public BTObjectToSet(SquadMember _member)
    {
        member = _member;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (!ObjectSetterManager.Instance || !member.GetSquadFunction().Equals(Squad.SquadFunction.EXTRA))
            yield break;

        ObjectToSet objectToSet = ObjectSetterManager.Instance.GetObjectToSet(bt.gameObject);

        if (objectToSet != null)
        {
            status = Status.SUCCESS;
        }

        yield break;
    }

}
