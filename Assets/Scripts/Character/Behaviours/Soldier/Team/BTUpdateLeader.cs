using System.Collections;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Controllers.Team;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTUpdateLeader : BTNode
    {
        private SquadMember squadMember;
        private TargetController targetController;

        public BTUpdateLeader(SquadMember _squadMember, TargetController _targetController)
        {
            squadMember = _squadMember;
            targetController = _targetController;
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
                {
                    targetController?.SetTarget(Leader.transform);
                    status = Status.SUCCESS;
                }
            }
            yield break;
        }
    }
}