using System.Collections;
using ZombieDiorama.Character.Handler.Team;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTObjectToSet : BTNode
    {
        private SquadMember member;
     
        public BTObjectToSet(SquadMember _member)
        {
            member = _member;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!ObjectSetterManager.Instance || !member.ExtraMember)
                yield break;

            ObjectToSet objectToSet = ObjectSetterManager.Instance.GetObjectToSet(bt.gameObject);

            if (objectToSet != null)
            {
                CurrentStatus = Status.SUCCESS;
            }
            yield break;
        }
    }
}