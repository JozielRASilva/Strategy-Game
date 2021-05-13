using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSeeSoldier : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        foreach (GameObject soldier in soldiers)
        {
            if (bt.gameObject == soldier) continue;
            if (Vector3.Distance(bt.transform.position, soldier.transform.position) < 5)
            {
                status = Status.SUCCESS;
                break;
            }
        }

        yield break;
    }
}
