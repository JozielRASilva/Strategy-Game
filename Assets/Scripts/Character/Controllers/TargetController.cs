using UnityEngine;

namespace ZombieDiorama.Character.Controllers
{
    public class TargetController : MonoBehaviour
    {

        private Transform currentTarget;

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