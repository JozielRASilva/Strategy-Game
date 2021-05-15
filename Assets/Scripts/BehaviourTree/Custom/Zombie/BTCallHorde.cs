using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCallHorde : BTNode
{

    private bool calling;

    private GameObject callCounter;

    private float i;

    public BTCallHorde (bool _calling, GameObject _callCounter)
    {
        calling = _calling;
        callCounter = _callCounter;
    }


    public override IEnumerator Run(BehaviourTree bt)
    {
        
        /*for (int i = 0; i <= 8; i++)
        {
            calling = true;

            callCounter.SetActive(true);

            status = Status.SUCCESS;

            Print("CHAMANDO");
            Debug.Log(i);

            if (i > 5)
            {
                calling = false;
                callCounter.SetActive(false);
                i = 0;
                Print("ZEROU");
            }
        }*/


        /*if (i < 8)
        {
            Call();

        }

        i += 1f;

        if (i == 8)
        {
            i = 0;
            calling = false;
            callCounter.SetActive(false);
        }*/



        yield break;
    }


    void Call()
    {
        calling = true;

        callCounter.SetActive(true);

        //GameObject.FindGameObjectsWithTag("Call").

        status = Status.SUCCESS;

        Print("CHAMANDO");
    }

}
