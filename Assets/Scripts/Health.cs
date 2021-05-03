using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Health : MonoBehaviour
{

    public string tagToTakeDamage = "Projectile";

    public int maxLife = 5;
    private int _currentLife;

    public float invincibilityTime = 0.2f;
    private float invincibilityTimeStamp;

    [OnInspectorGUI]
    private void Nodes()
    {
        GUILayout.Label($"Life: {_currentLife} / {maxLife}");
    }


    private void Awake()
    {
        _currentLife = maxLife;
    }


    public void TakeDamage(int value)
    {
        if (Time.time < invincibilityTimeStamp) return;

        if (_currentLife - value > 0)
        {
            _currentLife -= value;

            invincibilityTimeStamp = Time.time + invincibilityTime;
        }
        else
        {
            _currentLife = 0;
            Die();
        }

    }



    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToTakeDamage)
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }


}
