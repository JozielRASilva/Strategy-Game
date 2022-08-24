using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTUpdateObjectToSet : BTNode
    {
        private TargetHandler targetHandler;

        public BTUpdateObjectToSet(TargetHandler _targetHandler)
        {
            targetHandler = _targetHandler;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (!ObjectSetterManager.Instance)
                yield break;

            Transform objectToSet = ObjectSetterManager.Instance.GetObjectReference(bt.gameObject);

            if (objectToSet != null)
            {
                targetHandler.SetTarget(objectToSet);
                status = Status.SUCCESS;
            }

            yield break;
        }
    }
}