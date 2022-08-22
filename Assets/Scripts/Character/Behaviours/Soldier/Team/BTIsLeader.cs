using System.Collections;
using ZombieDiorama.Character.Controllers.Team;

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

            if (!TeamController.Instance) yield break;

            Squad.SquadFunction function = TeamController.Instance.GetSquadFunction(squadMember);

            if (function.Equals(Squad.SquadFunction.LEADER))
            {
                status = Status.SUCCESS;
            }
            yield break;
        }
    }
}