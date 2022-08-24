using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using ZombieDiorama.Extensions;
using UnityEngine.Serialization;

namespace ZombieDiorama.Character
{
    public class DamageOnTouch : KillableObject
    {
        [Title("Damage Info")]
        public int Damage = 1;
        public LayerMask Damageable;
        [FormerlySerializedAs("destroyOnDamage")] public bool DestroyOnDamage = false;
        public UnityEvent OnHit;

        private void OnTriggerEnter(Collider other)
        {
            if (UnityLayerMaskExtensions.Contains(Damageable, other.gameObject.layer))
            {
                Health _health = other.GetComponent<Health>();

                if (!_health.IsAlive()) return;
                _health.TakeDamage(Damage);

                OnHit?.Invoke();

                if (DestroyOnDamage)
                {
                    Destroy();
                }
            }
        }
    }
}