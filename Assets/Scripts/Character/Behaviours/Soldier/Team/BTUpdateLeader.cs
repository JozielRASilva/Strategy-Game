using System.Collections;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Controllers.Team;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTUpdateLeader : BTNode
    {
        private SquadMember _squadMember;
        private TargetController _targetController;

        public BTUpdateLeader(SquadMember squadMember, TargetController targetController)
        {
            _squadMember = squadMember;
            _targetController = targetController;
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
                {
                    _targetController?.SetTarget(Leader.transform);
                    CurrentStatus = Status.SUCCESS;
                }
            }
            yield break;
        }
    }
}