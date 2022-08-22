using System.Collections;
using ZombieDiorama.Character.Controllers.Team;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTObjectToSet : BTNode
    {
        private SquadMember _member;
     
        public BTObjectToSet(SquadMember member)
        {
            _member = member;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!ObjectSetterManager.Instance || !_member.ExtraMember)
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