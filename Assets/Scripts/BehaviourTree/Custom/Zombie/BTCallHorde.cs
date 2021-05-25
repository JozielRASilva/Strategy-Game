using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCallHorde : BTNode
{
    private GameObject callCounter;

    private float timeCalling;

    private EventCaller eventCaller;

    private float timeToEffectAgain;
    private float timeStamp;

    public BTCallHorde(GameObject _callCounter, float _timeCalling, EventCaller _eventCaller)
    {
        callCounter = _callCounter;
        timeCalling = _timeCalling;
        eventCaller = _eventCaller;

        timeToEffectAgain = timeCalling * 20;
    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        callCounter.SetActive(true);

        if (timeStamp < Time.time)
        {
            eventCaller.FirstCall();

            timeStamp = timeToEffectAgain + Time.deltaTime;
        }

        yield return new WaitForSeconds(timeCalling);

        callCounter.SetActive(false);

        status = Status.SUCCESS;

        yield break;
    }
}
