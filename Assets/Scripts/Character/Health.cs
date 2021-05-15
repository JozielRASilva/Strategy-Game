using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public int maxLife = 5;

    [Title("Damage")]
    public float invincibilityTime = 0.2f;

    [Title("Death")]
    public UnityEvent OnDie;

    private int _currentLife;
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
        OnDie?.Invoke();
        Destroy(gameObject);
    }

}