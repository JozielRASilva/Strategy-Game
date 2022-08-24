using UnityEngine;

namespace ZombieDiorama.Character.Handler
{
    public class TargetHandler : MonoBehaviour
    {
        [SerializeField] private Transform currentTarget;

        public Transform GetTarget()
        {
            return currentTarget;
        }

        public void SetTarget(Transform target)
        {
            currentTarget = target;
        }

    }
}