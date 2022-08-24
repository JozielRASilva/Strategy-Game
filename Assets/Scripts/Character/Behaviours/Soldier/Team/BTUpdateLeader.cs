using System.Collections;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Character.Handler.Team;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTUpdateLeader : BTNode
    {
        private SquadMember squadMember;
        private TargetHandler targetController;

        public BTUpdateLeader(SquadMember _squadMember, TargetHandler _targetController)
        {
            squadMember = _squadMember;
            targetController = _targetController;
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
                {
                    targetController?.SetTarget(Leader.transform);
                    CurrentStatus = Status.SUCCESS;
                }
            }
            yield break;
        }
    }
}