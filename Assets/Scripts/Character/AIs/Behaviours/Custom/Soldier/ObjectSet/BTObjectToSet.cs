using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.AIs.Controllers.Team;

namespace ZombieDiorama.Character.AIs.Behaviours.Custom.Soldier.ObjectSet
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