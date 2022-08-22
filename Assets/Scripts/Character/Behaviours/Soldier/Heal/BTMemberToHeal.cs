using System.Collections;
using System.Collections.Generic;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Controllers.Team;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTMemberToHeal : BTNode
    {
        private SquadMember _squadMember;
        private TargetController _targetController;

        public BTMemberToHeal(SquadMember squadMember, TargetController targetController)
        {
            _squadMember = squadMember;
            _targetController = targetController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!TeamController.Instance) yield break;

            List<SquadMember> members = TeamController.Instance.GetSquadMembers(_squadMember);

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
            _targetController.SetTarget(selected.transform);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}
