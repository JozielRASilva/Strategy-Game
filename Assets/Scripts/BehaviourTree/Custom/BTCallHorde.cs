using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCallHorde : BTNode
{

    private bool calling;

    private float i;

    public override IEnumerator Run(BehaviourTree bt)
    {
        
        if (i == 0)
        {
            Call();
        }

        i += 0.5f;

        if (i == 8)
        {
            i = 0;
        }



        yield break;
    }


    void Call()
    {
        calling = true;

        status = Status.SUCCESS;

        Print("CHAMANDO");
    }

}
