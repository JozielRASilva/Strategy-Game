using System.Collections;
using System.Collections.Generic;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Character.Handler.Team;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTMemberToHeal : BTNode
    {
        private SquadMember squadMember;
        private TargetHandler targetHandler;

        public BTMemberToHeal(SquadMember _squadMember, TargetHandler _targetHandler)
        {
            squadMember = _squadMember;
            targetHandler = _targetHandler;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!TeamHandler.Instance) yield break;

            List<SquadMember> members = TeamHandler.Instance.GetSquadMembers(squadMember);

            if (members == null) yield break;

            List<SquadMember> damagedMembers = members.FindAll(m => !m.Health.LifeIsCompleted()
            && m.Health.IsAlive());

            damagedMembers.RemoveAll(m => m.GetSquadFunction().Equals(Squad.SquadFunction.EXTRA));

            if (damagedMembers == null)
                yield break;

            if (damagedMembers.Count == 0)
                yield break;

            SquadMember selected = null;

            foreach (var member in damagedMembers)
            {
                if (selected)
                {
                    if (selected.Health.GetLife() < member.Health.GetLife())
                        selected = member;
                }
                else
                {
                    selected = member;
                }
            }
            targetHandler.SetTarget(selected.transform);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}
