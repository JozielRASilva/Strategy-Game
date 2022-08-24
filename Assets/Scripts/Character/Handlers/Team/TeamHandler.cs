using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace ZombieDiorama.Character.Handler.Team
{
    public class TeamHandler : Utilities.Patterns.Singleton<TeamHandler>
    {
        public List<Squad> squads = new List<Squad>();

        [Title("Squad Info")]
        public int MembersForSquad = 4;

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
                if (squad.ContainsMember(member))
                    return squad.GetFunction(member);
            }

            return Squad.SquadFunction.NONE;
        }

        public SquadMember GetSquadLeader(SquadMember member)
        {
            foreach (var squad in squads)
            {
                if (squad.ContainsMember(member))
                {
                    if (squad.Leader == member)
                        return null;
                    return squad.Leader;
                }
            }

            return null;
        }


        public List<SquadMember> GetSquadMembers(SquadMember member)
        {
            foreach (var squad in squads)
            {
                if (squad.ContainsMember(member))
                {
                    return squad.GetMembers(member);
                }
            }
            return null;
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

            List<Squad> squadsWithSpace = squads.FindAll(s => s.CanAddMember(CheckDefaultFunction(member), MembersForSquad, member)).ToList();

            Squad.SquadFunction function = CheckDefaultFunction(member);

            if (squadsWithSpace != null && member)
            {
                if (squadsWithSpace.Count != 0)
                {
                    if (!function.Equals(Squad.SquadFunction.EXTRA))
                    {
                        squadsWithSpace[0].AddMember(function, member);
                    }
                    else
                    {
                        Squad selected = null;

                        foreach (var squadWithSpace in squadsWithSpace)
                        {
                            if (selected != null)
                            {
                                if (squadWithSpace.ExtraMembers.Count < selected.ExtraMembers.Count)
                                {
                                    selected = squadWithSpace;
                                }

                            }
                            else
                            {
                                selected = squadWithSpace;
                            }
                        }
                        if (selected != null)
                        {
                            selected.AddMember(function, member);
                        }
                    }
                    return;
                }
            }

            if (member)
            {
                CreateSquad(member);
                return;
            }

        }

        public Squad.SquadFunction CheckDefaultFunction(SquadMember member)
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
}

