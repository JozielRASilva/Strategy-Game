using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMemberToHeal : BTNode
{

    private SquadMember squadMember;
    private TargetController targetController;

    public BTMemberToHeal(SquadMember _squadMember, TargetController _targetController)
    {
        squadMember = _squadMember;
        targetController = _targetController;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        if (!TeamManager.Instance) yield break;

        List<SquadMember> members = TeamManager.Instance.GetSquadMembers(squadMember);

        List<SquadMember> damagedMembers = members.FindAll(m => !m.health.LifeIsCompleted());

        if (damagedMembers == null)
            yield break;

        if (damagedMembers.Count == 0)
            yield break;


        SquadMember selected = null;

        foreach (var member in damagedMembers)
        {
            if (selected)
            {
                if (selected.health.GetLife() < member.health.GetLife())
                    selected = member;
            }
            else
            {
                selected = member;
            }
        }


        targetController.SetTarget(selected.transform);
        
        status = Status.SUCCESS;

        yield break;
    }
}

