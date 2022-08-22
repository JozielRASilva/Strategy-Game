using System.Collections;
using UnityEngine;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTCallHorde : BTNode
    {
        private GameObject _callCounter;
        private float _timeCalling;
        private EventCaller _eventCaller;
        private float _timeToEffectAgain;
        private float _timeStamp;

        public BTCallHorde(GameObject callCounter, float timeCalling, EventCaller eventCaller)
        {
            _callCounter = callCounter;
            _timeCalling = timeCalling;
            _eventCaller = eventCaller;
            _timeToEffectAgain = this._timeCalling * 100;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            _callCounter.SetActive(true);

            if (_timeStamp < Time.time)
            {
                _eventCaller.FirstCall();
                _timeStamp = _timeToEffectAgain + Time.deltaTime;
            }

            yield return new WaitForSeconds(_timeCalling);

            _callCounter.SetActive(false);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}