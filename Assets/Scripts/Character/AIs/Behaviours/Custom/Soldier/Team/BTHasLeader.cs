using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.AIs.Controllers.Team;

namespace ZombieDiorama.Character.AIs.Behaviours.Custom.Soldier.Team
{
    public class BTHasLeader : BTNode
    {
        private SquadMember squadMember;

        public BTHasLeader(SquadMember _squadMember)
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
                    status = Status.SUCCESS;
            }
            yield break;
        }
    }
}