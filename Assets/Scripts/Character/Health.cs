using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class Health : KillableObject
{

    [Title("Life Info")]
    public int maxLife = 5;

    [Title("Damage")]
    public float invincibilityTime = 0.2f;
    public UnityEvent OnDamaged;

    [Title("Heal")]
    public UnityEvent OnHealed;

    public UnityEvent OnDie;

    private int _currentLife;
    private float invincibilityTimeStamp;

    [OnInspectorGUI]
    private void Nodes()
    {
        GUILayout.Label($"Life: {_currentLife} / {maxLife}");
    }


    [Title("Buttons Actions")]

    [Button("Heal")]
    public void BHeal() => AddLife(1);

    [Button("Hit")]
    public void BHit() => TakeDamage(1);

    [Button("Kill")]
    public void BKill() => TakeDamage(maxLife);

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

            OnDamaged?.Invoke();
        }
        else
        {
            _currentLife = 0;
            Destroy();
        }

    }

    public void AddLife(int value)
    {
        if (_currentLife + value < maxLife)
        {
            _currentLife += value;
        }
        else
        {
            _currentLife = maxLife;
        }
        OnHealed?.Invoke();
    }

    public int GetLife()
    {
        return _currentLife;
    }

    public bool LifeIsCompleted()
    {
        if (_currentLife == maxLife) return true;
        else return false;

    }

}