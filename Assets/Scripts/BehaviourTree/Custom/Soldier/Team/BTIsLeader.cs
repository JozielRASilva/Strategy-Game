using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIsLeader : BTNode
{

    private SquadMember squadMember;

    public BTIsLeader(SquadMember _squadMember)
    {
        squadMember = _squadMember;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (!TeamManager.Instance) yield break;

        Squad.SquadFunction function = TeamManager.Instance.GetSquadFunction(squadMember);

        if (function.Equals(Squad.SquadFunction.LEADER))
        {
            status = Status.SUCCESS;
        }

        yield break;
    }
}