using System.Collections;
using ZombieDiorama.Character.Handler.Team;

namespace ZombieDiorama.Character.Behaviours.Soldier
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
            CurrentStatus = Status.FAILURE;

            if (!TeamHandler.Instance) yield break;

            Squad.SquadFunction function = TeamHandler.Instance.GetSquadFunction(squadMember);

            if (!function.Equals(Squad.SquadFunction.LEADER) && !function.Equals(Squad.SquadFunction.NONE))
            {
                SquadMember Leader = TeamHandler.Instance.GetSquadLeader(squadMember);
                if (Leader)
                    CurrentStatus = Status.SUCCESS;
            }
            yield break;
        }
    }
}