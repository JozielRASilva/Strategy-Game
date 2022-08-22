using System.Collections;
using ZombieDiorama.Character.Controllers.Team;

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
            status = Status.FAILURE;

            if (!TeamController.Instance) yield break;

            Squad.SquadFunction function = TeamController.Instance.GetSquadFunction(squadMember);

            if (!function.Equals(Squad.SquadFunction.LEADER) && !function.Equals(Squad.SquadFunction.NONE))
            {
                SquadMember Leader = TeamController.Instance.GetSquadLeader(squadMember);
                if (Leader)
                    status = Status.SUCCESS;
            }
            yield break;
        }
    }
}