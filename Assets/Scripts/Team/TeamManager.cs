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

    public Squad.SquadFunction GetSquadFunction(SquadMember member)
    {
        OrganizeMemberOnSquads(member);

        foreach (var squad in squads)
        {
            return squad.GetFunction(member);
        }

        return Squad.SquadFunction.NONE;
    }

    public void OrganizeMemberOnSquads(SquadMember member)
    {

        // Squad com vaga?
        Squad squadWithSpace = squads.Find(s => s.CanAddMember(Squad.SquadFunction.MEMBER, MembersForSquad, member));

        if (squadWithSpace != null)
        {
            // Adicionar como membro
            squadWithSpace.AddMember(Squad.SquadFunction.MEMBER, member);
            return;
        }
        else
        {
            // Criar squad se não houver um
            CreateSquad(member);
            return;
        }




        // Validate Squads

    }

    public void CreateSquad(SquadMember member)
    {
        Squad squad = new Squad();
        squad.Leader = member;

        squads.Add(squad);
    }

}

