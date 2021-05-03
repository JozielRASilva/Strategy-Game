using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform target;
    public float minDistance = 0.5f;

    private void Update()
    {
        SetTarget();
    }

    public void SetTarget()
    {
        if (target)
            agent.SetDestination(target.position);


    }

}
