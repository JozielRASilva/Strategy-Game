using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTUpdateLeader : BTNode
{

    private SquadMember squadMember;
    private TargetController targetController;

    public BTUpdateLeader(SquadMember _squadMember, TargetController _targetController)
    {
        squadMember = _squadMember;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (!TeamManager.Instance) yield break;

        Squad.SquadFunction function = TeamManager.Instance.GetSquadFunction(squadMember);

        if (!function.Equals(Squad.SquadFunction.LEADER) && !function.Equals(Squad.SquadFunction.NONE))
        {

            SquadMember Leader = TeamManager.Instance.GetSquadLeader(squadMember);

            if (Leader)
            {
                targetController.SetTarget(Leader.transform);
                status = Status.SUCCESS;
            }
        }

        yield break;
    }
}

