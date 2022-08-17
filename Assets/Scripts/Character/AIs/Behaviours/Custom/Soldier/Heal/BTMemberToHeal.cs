using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.AIs.Controllers;
using ZombieDiorama.Character.AIs.Controllers.Team;

namespace ZombieDiorama.Character.AIs.Behaviours.Custom.Soldier.Heal
{
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

            if (members == null) yield break;

            List<SquadMember> damagedMembers = members.FindAll(m => !m.health.LifeIsCompleted()
            && m.health.IsAlive());

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
}
