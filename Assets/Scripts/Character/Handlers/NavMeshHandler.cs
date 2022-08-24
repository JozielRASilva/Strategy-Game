using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace ZombieDiorama.Character.Handler
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshHandler : MonoBehaviour
    {

        [Header("Animation")]
        [FormerlySerializedAs("xSpeed")] public string XSpeed = "xSpeed";
        [FormerlySerializedAs("animator")] public Animator Animator;

        private NavMeshAgent agent;
        private Transform currentTarget;
        private bool canMove;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            if (!Animator) Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            SetDestinationToTarget();
            if (canMove)
                SetAnimationValues();
        }

        private void SetAnimationValues()
        {
            if (Animator)
                Animator.SetFloat(XSpeed, agent.velocity.magnitude);
        }

        public void SetDestinationToTarget()
        {
            if (currentTarget && canMove)
            {
                agent.SetDestination(currentTarget.position);
                agent.isStopped = false;
            }
            else
            {
                agent.isStopped = true;
            }
        }

        private bool CanMoveToDestination()
        {
            if (currentTarget && canMove) return true;
            return false;
        }

        public void SetTarget(Transform transform, float speed, float distance)
        {
            currentTarget = transform;

            agent.speed = speed;

            agent.stoppingDistance = distance;

            canMove = true;
        }

        public void StartMove()
        {
            canMove = true;
        }

        public void StopMove()
        {
            canMove = false;
            agent.SetDestination(transform.position);
            Animator.SetFloat(XSpeed, 0);
        }

    }
}