using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshController : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform _currentTarget;

    private bool CanMove;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SetDestinationToTarget();
    }

    public void SetDestinationToTarget()
    {
        if (_currentTarget)
            agent.SetDestination(_currentTarget.position);
    }

    private bool CanMoveToDestination()
    {
        if (_currentTarget && CanMove) return true;
        return false;
    }

    public void SetTarget(Transform transform, float speed)
    {
        _currentTarget = transform;

        agent.speed = speed;

        CanMove = true;
    }

    public void StartMove()
    {
        CanMove = true;
    }

    public void StopMove()
    {
        CanMove = false;
    }

}

