using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{

    public int Damage = 1;

    public LayerMask Damageable;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (UnityExtensions.Contains(Damageable, other.gameObject.layer))
        {

            Health _health = other.GetComponent<Health>();

            _health.TakeDamage(Damage);

        }
    }
}
