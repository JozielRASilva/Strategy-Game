using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSeeSoldier : BTNode
{
    private TargetController targetSoldier;

    private float distanceView;


    public BTSeeSoldier (TargetController _targetSoldier, float _distanceView)
    {
        targetSoldier = _targetSoldier;
        distanceView = _distanceView;
    }

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        foreach (GameObject soldier in soldiers)
        {
            if (bt.gameObject == soldier) continue;
            if (Vector3.Distance(bt.transform.position, soldier.transform.position) < distanceView)
            {
                targetSoldier.SetTarget(soldier.transform);

                status = Status.SUCCESS;
                break;
            }
        }

        yield break;
    }
}
