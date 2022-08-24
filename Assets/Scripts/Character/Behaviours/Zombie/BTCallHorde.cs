using System.Collections;
using UnityEngine;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTCallHorde : BTNode
    {
        private GameObject callCounter;
        private float timeCalling;
        private EventCaller onCallHorde;
        private float timeToEffectAgain;
        private float timeStamp;

        public BTCallHorde(GameObject _callCounter, float _timeCalling, EventCaller _oncallHorde)
        {
            callCounter = _callCounter;
            timeCalling = _timeCalling;
            onCallHorde = _oncallHorde;
            timeToEffectAgain = timeCalling * 100;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            callCounter.SetActive(true);

            if (timeStamp < Time.time)
            {
                onCallHorde.FirstCall();
                timeStamp = timeToEffectAgain + Time.deltaTime;
            }

            yield return new WaitForSeconds(timeCalling);

            callCounter.SetActive(false);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}