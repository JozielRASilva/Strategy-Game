using System.Collections;
using ZombieDiorama.Character.Handler.Team;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTIsLeader : BTNode
    {
        private SquadMember squadMember;

        public BTIsLeader(SquadMember _squadMember)
        {
            squadMember = _squadMember;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (!TeamHandler.Instance) yield break;

            Squad.SquadFunction function = TeamHandler.Instance.GetSquadFunction(squadMember);

            if (function.Equals(Squad.SquadFunction.LEADER))
            {
                status = Status.SUCCESS;
            }
            yield break;
        }
    }
}