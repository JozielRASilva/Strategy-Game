using System.Collections;
using ZombieDiorama.Character.Controllers.Team;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTObjectToSet : BTNode
    {
        SquadMember member;
     
        public BTObjectToSet(SquadMember _member)
        {
            member = _member;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (!ObjectSetterManager.Instance || !member.ExtraMember)
                yield break;

            ObjectToSet objectToSet = ObjectSetterManager.Instance.GetObjectToSet(bt.gameObject);

            if (objectToSet != null)
            {
                status = Status.SUCCESS;
            }
            yield break;
        }
    }
}