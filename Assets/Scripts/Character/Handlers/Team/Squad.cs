using System.Collections.Generic;
using System;

namespace ZombieDiorama.Character.Handler.Team
{
    [Serializable]
    public class Squad
    {
        public SquadMember Leader;

        public List<SquadMember> Members = new List<SquadMember>();

        public List<SquadMember> ExtraMembers = new List<SquadMember>();

        public enum SquadFunction { LEADER, MEMBER, EXTRA, NONE }

        public bool IsValid()
        {
            if (Leader) return true;
            else return false;
        }

        public bool ContainsMembers()
        {
            if (!Leader && Members.Count == 0 && ExtraMembers.Count == 0) return false;
            else return true;
        }

        public bool ContainsMember(SquadMember member)
        {
            if (Leader)
                if (Leader.Equals(member))
                    return true;

            if (Members.Contains(member)) return true;

            if (ExtraMembers.Contains(member)) return true;

            return false;
        }

        public bool CanAddMember(SquadFunction function, int MembersForSquad, SquadMember newMember)
        {

            if (Leader)
                if (Leader.Equals(newMember))
                    return false;

            if (Members.Contains(newMember)) return false;

            if (ExtraMembers.Contains(newMember)) return false;

            if (function.Equals(SquadFunction.MEMBER))
            {
                if (Members.Count >= MembersForSquad - 1) return false;
            }
            else if (function.Equals(SquadFunction.EXTRA))
            {
                if (!Leader) return false;
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

            Members.RemoveAll(m => m.Equals(null));
            ExtraMembers.RemoveAll(m => m.Equals(null));
        }

        public void RemoveMember(SquadMember member)
        {
            if (Leader)
                if (Leader.Equals(member))
                    Leader = null;

            if (Members.Contains(member)) Members.Remove(member);

            if (ExtraMembers.Contains(member)) ExtraMembers.Remove(member);

            UpdateLeader();
        }

        public void UpdateLeader(List<SquadMember> others = null)
        {
            if (Leader) return;

            SquadMember selected = null;

            foreach (var member in Members)
            {
                if (member)
                {
                    selected = member;
                    break;
                }
            }

            if (selected) Members.Remove(selected);

            if (others != null)
                if (!selected)
                {
                    foreach (var other in others)
                    {
                        if (other)
                        {
                            selected = other;
                            break;
                        }
                    }
                }

            if (selected) Leader = selected;
        }

        public SquadFunction GetFunction(SquadMember member)
        {
            if (member)
                if (member.Equals(Leader)) return SquadFunction.LEADER;

            if (Members != null && member)
                if (Members.Contains(member)) return SquadFunction.MEMBER;

            if (ExtraMembers != null && member)
                if (ExtraMembers.Contains(member)) return SquadFunction.EXTRA;

            return SquadFunction.NONE;
        }

        public List<SquadMember> GetMembers(SquadMember member)
        {
            List<SquadMember> squadMembers = new List<SquadMember>();

            if (member)
                if (!member.Equals(Leader)) squadMembers.Add(Leader);

            if (Members != null && member)
            {
                foreach (var _member in Members)
                {
                    if (!_member.Equals(member))
                    {
                        squadMembers.Add(_member);
                    }
                }
            }

            if (ExtraMembers != null && member)
            {
                foreach (var _member in ExtraMembers)
                {
                    if (!_member.Equals(member))
                    {
                        squadMembers.Add(_member);
                    }
                }
            }

            return squadMembers;
        }

        public bool Full(int MembersForSquad)
        {
            if (!Leader) return false;

            if (Members.Count < MembersForSquad - 1) return false;

            return true;
        }
    }
}
