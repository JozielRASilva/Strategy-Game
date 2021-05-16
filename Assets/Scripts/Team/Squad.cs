using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;


[Serializable]
public class Squad
{
    public SquadMember Leader;

    public List<SquadMember> Members;

    public List<SquadMember> ExtraMembers;

    public enum SquadFunction { LEADER, MEMBER, EXTRA, NONE }

    public bool IsValid()
    {
        if (Leader) return true;
        else return false;
    }

    public bool CanAddMember(SquadFunction function, int MembersForSquad, SquadMember newMember)
    {

        if (Leader.Equals(newMember))
            return false;

        if (Members.Contains(newMember)) return false;

        if (ExtraMembers.Contains(newMember)) return false;

        if (function.Equals(SquadFunction.MEMBER))
        {
            if (Members.Count >= MembersForSquad - 1) return false;
        }

        return true;
    }

    public void AddMember(SquadFunction function, SquadMember newMember)
    {
        if (function.Equals(SquadFunction.MEMBER))
        {
            Members.Add(newMember);
        }
        else if (function.Equals(SquadFunction.EXTRA))
        {
            ExtraMembers.Add(newMember);
        }
    }

    public void UpdateLeader(List<SquadMember> others = null)
    {
        if (Leader) return;

        SquadMember selected = null;

        foreach (var member in Members)
        {
            if (member) selected = member;
        }

        if (selected) Members.Remove(selected);

        if (others != null)
            if (!selected)
            {
                foreach (var other in others)
                {
                    if (other) selected = other;
                }
            }

        if (selected) Leader = selected;

    }

    public SquadFunction GetFunction(SquadMember member)
    {
        if (member.Equals(Leader)) return SquadFunction.LEADER;

        if (Members.Contains(member)) return SquadFunction.MEMBER;

        if (Members.Contains(member)) return SquadFunction.EXTRA;

        return SquadFunction.NONE;
    }

}
