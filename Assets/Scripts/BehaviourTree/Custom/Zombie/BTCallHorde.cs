using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCallHorde : BTNode
{
    private GameObject callCounter;

    private float timeCalling;

    public BTCallHorde (GameObject _callCounter, float _timeCalling)
    {
        callCounter = _callCounter;
        timeCalling = _timeCalling;
    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        callCounter.SetActive(true);

        yield return new WaitForSeconds(timeCalling);

        callCounter.SetActive(false);

        status = Status.SUCCESS;

        yield break;
    }
}
