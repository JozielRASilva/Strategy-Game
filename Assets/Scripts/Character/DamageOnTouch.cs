using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class DamageOnTouch : MonoBehaviour
{

    public int Damage = 1;

    public LayerMask Damageable;

    public bool destroyOnDamage = false;

    [ShowIf("destroyOnDamage", true)]
    public UnityEvent OnDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (UnityExtensions.Contains(Damageable, other.gameObject.layer))
        {

            Health _health = other.GetComponent<Health>();

            _health.TakeDamage(Damage);

            if (destroyOnDamage)
            {
                Destroy();
            }

        }
    }


    private void Destroy()
    {
        OnDestroy?.Invoke();

        gameObject.SetActive(false);
    }
}
