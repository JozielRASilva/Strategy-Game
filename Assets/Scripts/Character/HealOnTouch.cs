using UnityEngine;
using UnityEngine.Events;
using ZombieDiorama.Extensions;
using ZombieDiorama.Utilities.Primitives;

namespace ZombieDiorama.Character
{
    public class HealOnTouch : MonoBehaviour
    {
        public SOInt Heal;
        public LayerMask Healable;
        public UnityEvent OnHeal;
        private Health health;

        private void Awake()
        {
            health = GetComponentInParent<Health>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.Equals(health.gameObject)) return;

            if (UnityLayerMaskExtensions.Contains(Healable, other.gameObject.layer))
            {
                Health _health = other.GetComponent<Health>();
                if (!_health.IsAlive()) return;
                _health.AddLife(Heal.Value);
                OnHeal?.Invoke();
            }
        }
    }
}