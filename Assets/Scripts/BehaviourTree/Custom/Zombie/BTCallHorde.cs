using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCallHorde : BTNode
{
    private GameObject callCounter;
    private bool calling;

    private float timeCalling;

    public BTCallHorde (GameObject _callCounter, bool _calling, float _timeCalling)
    {
        callCounter = _callCounter;
        calling = _calling;
        timeCalling = _timeCalling;
    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        callCounter.SetActive(true);
        calling = true;

        yield return new WaitForSeconds(timeCalling);

        callCounter.SetActive(false);

        status = Status.SUCCESS;

        yield break;
    }
}
