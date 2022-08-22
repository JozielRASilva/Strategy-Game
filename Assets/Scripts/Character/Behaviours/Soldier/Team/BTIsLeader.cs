using System.Collections;
using ZombieDiorama.Character.Controllers.Team;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTIsLeader : BTNode
    {
        private SquadMember _squadMember;

        public BTIsLeader(SquadMember squadMember)
        {
            _squadMember = squadMember;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!TeamController.Instance) yield break;

            Squad.SquadFunction function = TeamController.Instance.GetSquadFunction(_squadMember);

            if (function.Equals(Squad.SquadFunction.LEADER))
            {
                CurrentStatus = Status.SUCCESS;
            }
            yield break;
        }
    }
}