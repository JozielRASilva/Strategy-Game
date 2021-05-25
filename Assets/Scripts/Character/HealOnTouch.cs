using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealOnTouch : MonoBehaviour
{

    public int Heal = 1;

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

        if (UnityExtensions.Contains(Healable, other.gameObject.layer))
        {

            Health _health = other.GetComponent<Health>();
            if (!_health.IsAlive()) return;
            _health.AddLife(Heal);

            OnHeal?.Invoke();

        }
    }

}
