using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTUpdateObjectToSet : BTNode
    {
        private TargetController targetController;

        public BTUpdateObjectToSet(TargetController _targetController)
        {
            targetController = _targetController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            if (!ObjectSetterManager.Instance)
                yield break;

            Transform objectToSet = ObjectSetterManager.Instance.GetObjectReference(bt.gameObject);

            if (objectToSet != null)
            {
                targetController.SetTarget(objectToSet);
                status = Status.SUCCESS;
            }

            yield break;
        }
    }
}