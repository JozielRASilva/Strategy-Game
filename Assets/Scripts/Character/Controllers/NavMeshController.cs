using UnityEngine;
using UnityEngine.AI;

namespace ZombieDiorama.Character.Controllers
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshController : MonoBehaviour
    {

        [Header("Animation")]
        public string xSpeed = "xSpeed";
        public Animator animator;

        private NavMeshAgent agent;
        private Transform currentTarget;
        private bool canMove;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            if (!animator) animator = GetComponent<Animator>();
        }

        private void Update()
        {
            SetDestinationToTarget();
            if (canMove)
                SetAnimationValues();
        }

        private void SetAnimationValues()
        {
            if (animator)
                animator.SetFloat(xSpeed, agent.velocity.magnitude);
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
            animator.SetFloat(xSpeed, 0);
        }

    }
}