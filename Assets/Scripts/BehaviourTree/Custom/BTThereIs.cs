using UnityEngine;
using System.Collections;

public class BTThereIs : BTNode
{

    public string target;

    public BTThereIs(string _target)
    {
        target = _target;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        if (GameObject.FindGameObjectWithTag(target))
        {

            status = Status.SUCCESS;

        }
        else
        {
            status = Status.FAILURE;
        }

        yield break;
    }


}