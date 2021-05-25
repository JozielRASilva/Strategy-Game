using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCallHorde : BTNode
{
    private GameObject callCounter;

    private float timeCalling;

    private EventCaller eventCaller;

    public BTCallHorde (GameObject _callCounter, float _timeCalling, EventCaller _eventCaller)
    {
        callCounter = _callCounter;
        timeCalling = _timeCalling;
        eventCaller = _eventCaller;
    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        callCounter.SetActive(true);
        
        eventCaller.FirstCall();

        yield return new WaitForSeconds(timeCalling);

        callCounter.SetActive(false);

        status = Status.SUCCESS;

        yield break;
    }
}
