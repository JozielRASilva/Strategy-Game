using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class DamageOnTouch : KillableObject
{
    [Title("Damage Info")]
    public int Damage = 1;

    public LayerMask Damageable;

    public bool destroyOnDamage = false;

    public UnityEvent OnHit;

    private void OnTriggerEnter(Collider other)
    {
        if (UnityLayerMaskExtensions.Contains(Damageable, other.gameObject.layer))
        {

            Health _health = other.GetComponent<Health>();

            if (!_health.IsAlive()) return;
            _health.TakeDamage(Damage);

            OnHit?.Invoke();

            if (destroyOnDamage)
            {
                Destroy();
            }

        }
    }

}
