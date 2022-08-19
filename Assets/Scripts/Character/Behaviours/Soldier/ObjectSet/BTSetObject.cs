using System.Collections;
using UnityEngine;
using ZombieDiorama.ObjectPlacer;
using ZombieDiorama.Utilities;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTSetObject : BTNode
    {
        private float delayToSet;
        private EventCaller OnSetting;
        private EventCaller OnSet;

        public BTSetObject(float _delayToSet, EventCaller _OnSetting, EventCaller _OnSet)
        {
            delayToSet = _delayToSet;
            OnSetting = _OnSetting;
            OnSet = _OnSet;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.RUNNING;

            if (!ObjectSetterManager.Instance)
                yield break;

            ObjectToSet objectToSet = ObjectSetterManager.Instance.GetObjectToSet(bt.gameObject);

            if (objectToSet != null)
            {
                if (OnSetting)
                    OnSetting.FirstCall();

                yield return new WaitForSeconds(delayToSet);

                if (OnSetting)
                    OnSetting.SecondCall();

                if (OnSet)
                    OnSet.FirstCall();

                ObjectSetterManager.Instance.SetObject(objectToSet);

                status = Status.SUCCESS;
            }
            else
            {
                status = Status.SUCCESS;
            }
            yield break;
        }
    }
}