using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshController : MonoBehaviour
{

    [Header("Animation")]
    public string xSpeed = "xSpeed";

    public Animator animator;


    private NavMeshAgent agent;

    private Transform _currentTarget;

    [SerializeField]
    private bool CanMove;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (!animator) animator = GetComponent<Animator>();

    }

    private void Update()
    {
        SetDestinationToTarget();

        if(CanMove)
        SetAnimationValues();
    }

    private void SetAnimationValues()
    {
        if(animator)
        animator.SetFloat(xSpeed, agent.velocity.magnitude);

    }

    public void SetDestinationToTarget()
    {
        if (_currentTarget && CanMove)
        {
            agent.SetDestination(_currentTarget.position);
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    private bool CanMoveToDestination()
    {
        if (_currentTarget && CanMove) return true;
        return false;
    }

    public void SetTarget(Transform transform, float speed, float distance)
    {
        _currentTarget = transform;

        agent.speed = speed;

        agent.stoppingDistance = distance;

        CanMove = true;
    }

    public void StartMove()
    {
        CanMove = true;
    }

    public void StopMove()
    {
        CanMove = false;
        agent.SetDestination(transform.position);
        animator.SetFloat(xSpeed, 0);
    }

}

