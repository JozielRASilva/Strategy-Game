using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using ZombieDiorama.Extensions;
using UnityEngine.Serialization;
using ZombieDiorama.Utilities.Primitives;

namespace ZombieDiorama.Character
{
    public class DamageOnTouch : KillableObject
    {
        [Title("Damage Info")]
        public SOInt Damage;

        public LayerMask Damageable;
        [FormerlySerializedAs("destroyOnDamage")] public bool DestroyOnDamage = false;
        public UnityEvent OnHit;

        private void OnTriggerEnter(Collider other)
        {
            if (UnityLayerMaskExtensions.Contains(Damageable, other.gameObject.layer))
            {
                Health _health = other.GetComponent<Health>();

                if (!_health.IsAlive()) return;
                _health.TakeDamage(Damage.Value);

                OnHit?.Invoke();

                if (DestroyOnDamage)
                {
                    Destroy();
                }
            }
        }
    }
}