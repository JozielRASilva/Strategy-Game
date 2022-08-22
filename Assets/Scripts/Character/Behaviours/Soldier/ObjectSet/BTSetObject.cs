using System.Collections;
using UnityEngine;
using ZombieDiorama.ObjectPlacer;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTSetObject : BTNode
    {
        private float _delayToSet;
        private EventCaller _onSetting;
        private EventCaller _onSet;

        public BTSetObject(float delayToSet, EventCaller onSetting, EventCaller onSet)
        {
            _delayToSet = delayToSet;
            _onSetting = onSetting;
            _onSet = onSet;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;

            if (!ObjectSetterManager.Instance)
                yield break;

            ObjectToSet objectToSet = ObjectSetterManager.Instance.GetObjectToSet(bt.gameObject);

            if (objectToSet != null)
            {
                if (_onSetting)
                    _onSetting.FirstCall();

                yield return new WaitForSeconds(_delayToSet);

                if (_onSetting)
                    _onSetting.SecondCall();

                if (_onSet)
                    _onSet.FirstCall();

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