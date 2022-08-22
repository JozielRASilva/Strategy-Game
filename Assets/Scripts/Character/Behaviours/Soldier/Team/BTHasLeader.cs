using System.Collections;
using ZombieDiorama.Character.Controllers.Team;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTHasLeader : BTNode
    {
        private SquadMember _squadMember;

        public BTHasLeader(SquadMember squadMember)
        {
            _squadMember = squadMember;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!TeamController.Instance) yield break;

            Squad.SquadFunction function = TeamController.Instance.GetSquadFunction(_squadMember);

            if (!function.Equals(Squad.SquadFunction.LEADER) && !function.Equals(Squad.SquadFunction.NONE))
            {
                SquadMember Leader = TeamController.Instance.GetSquadLeader(_squadMember);
                if (Leader)
                    CurrentStatus = Status.SUCCESS;
            }
            yield break;
        }
    }
}