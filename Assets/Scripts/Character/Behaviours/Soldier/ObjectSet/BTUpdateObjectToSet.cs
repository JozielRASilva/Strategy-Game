using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTUpdateObjectToSet : BTNode
    {
        private TargetController _targetController;

        public BTUpdateObjectToSet(TargetController targetController)
        {
            _targetController = targetController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            if (!ObjectSetterManager.Instance)
                yield break;

            Transform objectToSet = ObjectSetterManager.Instance.GetObjectReference(bt.gameObject);

            if (objectToSet != null)
            {
                _targetController.SetTarget(objectToSet);
                CurrentStatus = Status.SUCCESS;
            }

            yield break;
        }
    }
}