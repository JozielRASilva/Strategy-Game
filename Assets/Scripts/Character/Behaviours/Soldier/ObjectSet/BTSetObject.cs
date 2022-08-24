using System.Collections;
using UnityEngine;
using ZombieDiorama.ObjectPlacer;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTSetObject : BTNode
    {
        private float delayToSet;
        private EventCaller onSetting;
        private EventCaller onSet;

        public BTSetObject(float _delayToSet, EventCaller _onSetting, EventCaller _onSet)
        {
            delayToSet = _delayToSet;
            onSetting = _onSetting;
            onSet = _onSet;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;

            if (!ObjectSetterManager.Instance)
                yield break;

            ObjectToSet objectToSet = ObjectSetterManager.Instance.GetObjectToSet(bt.gameObject);

            if (objectToSet != null)
            {
                if (onSetting)
                    onSetting.FirstCall();

                yield return new WaitForSeconds(delayToSet);

                if (onSetting)
                    onSetting.SecondCall();

                if (onSet)
                    onSet.FirstCall();

                ObjectSetterManager.Instance.SetObject(objectToSet);

                CurrentStatus = Status.SUCCESS;
            }
            else
            {
                CurrentStatus = Status.SUCCESS;
            }
            yield break;
        }
    }
}