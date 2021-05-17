using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class TeamManager : MonoBehaviour
{

    public static TeamManager Instance;

    public List<Squad> squads = new List<Squad>();

    [Title("Squad Info")]
    public int MembersForSquad = 4;


    private void Awake()
    {
        Instance = this;
    }

    [Button("Validate")]
    public void GetFunctionCall()
    {
        GetSquadFunction(null);
    }

    public Squad.SquadFunction GetSquadFunction(SquadMember member)
    {
        OrganizeMemberOnSquads(member);

        ValidateMembersOnSquads();

        foreach (var squad in squads)
        {
            return squad.GetFunction(member);
        }

        return Squad.SquadFunction.NONE;
    }

    public void RemoveFromSquad(SquadMember member)
    {

        foreach (var squad in squads)
        {
            if (squad.ContainsMember(member))
            {
                squad.RemoveMember(member);
                break;
            }
        }

        ValidateMembersOnSquads();
    }

    private void OrganizeMemberOnSquads(SquadMember member)
    {
        bool hasSquad = squads.Exists(s => !s.GetFunction(member).Equals(Squad.SquadFunction.NONE));

        if (hasSquad) return;

        Squad squadWithSpace = squads.Find(s => s.CanAddMember(GetFunction(member), MembersForSquad, member));

        Squad.SquadFunction function = GetFunction(member);

        if (squadWithSpace != null && member)
        {
            squadWithSpace.AddMember(function, member);
            return;
        }
        else if (member)
        {
            CreateSquad(member);
            return;
        }
    }

    public Squad.SquadFunction GetFunction(SquadMember member)
    {
        if (member.ExtraMember) return Squad.SquadFunction.EXTRA;
        else return Squad.SquadFunction.MEMBER;
    }

    private void ValidateMembersOnSquads()
    {
        if (squads.Count == 0) return;

        List<Squad> squadsOneLeader = new List<Squad>();

        foreach (var squad in squads)
        {
            if (!squad.IsValid())
            {
                squad.UpdateLeader();
            }

            if (squad.Members.Count == 0 && squad.ExtraMembers.Count == 0)
            {
                squadsOneLeader.Add(squad);
            }
        }


        bool existFreeSquad = squads.Exists(s => !s.Full(MembersForSquad) && !squadsOneLeader.Contains(s));

        if (existFreeSquad)
        {
            squads.RemoveAll(s => squadsOneLeader.Contains(s));
        }


        // Delete squad
        squads.RemoveAll(s => !s.Leader && s.Members.Count == 0);

    }


    private void CreateSquad(SquadMember member)
    {
        if (member.ExtraMember) return;

        Squad squad = new Squad();
        squad.Leader = member;

        squads.Add(squad);
    }

}

