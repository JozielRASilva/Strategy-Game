using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnTouch : MonoBehaviour
{

    public int Heal = 1;

    public LayerMask Healable;

    private void OnTriggerEnter(Collider other)
    {
        if (UnityExtensions.Contains(Healable, other.gameObject.layer))
        {

            Health _health = other.GetComponent<Health>();

            _health.AddLife(Heal);

        }
    }

}
