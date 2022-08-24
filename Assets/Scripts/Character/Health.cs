using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System;
using UnityEngine.Serialization;

namespace ZombieDiorama.Character
{
    public class Health : KillableObject
    {
        [Title("Life Info")]
        [FormerlySerializedAs("maxLife")] public int MaxLife = 5;

        [Title("Damage")]
        [FormerlySerializedAs("invincibilityTime")] public float InvincibilityTime = 0.2f;
        public UnityEvent OnDamaged;

        [Title("Heal")]
        public UnityEvent OnHealed;

        public Action OnChange;

        private int _currentLife;
        private float invincibilityTimeStamp;

        [OnInspectorGUI]
        private void Nodes()
        {
            GUILayout.Label($"Life: {_currentLife} / {MaxLife}");
        }

        [Title("Buttons Actions")]

        [Button("Heal")]
        private void BHeal() => AddLife(1);

        [Button("Hit")]
        private void BHit() => TakeDamage(1);

        [Button("Kill")]
        private void BKill() => TakeDamage(MaxLife);

        protected void Awake()
        {
            _currentLife = MaxLife;
        }

        public void TakeDamage(int value)
        {
            if (Time.time < invincibilityTimeStamp) return;

            if (_currentLife - value > 0)
            {
                _currentLife -= value;
                invincibilityTimeStamp = Time.time + InvincibilityTime;

                OnDamaged?.Invoke();
                OnChange?.Invoke();
            }
            else
            {
                invincibilityTimeStamp = Time.time + DelayToDestroy;
                _currentLife = 0;

                OnChange?.Invoke();
                Destroy();
            }
        }

        public void AddLife(int value)
        {
            if (_currentLife + value < MaxLife)
            {
                _currentLife += value;
            }
            else
            {
                _currentLife = MaxLife;
            }
            OnChange?.Invoke();
            OnHealed?.Invoke();
        }

        public int GetLife()
        {
            return _currentLife;
        }

        public bool LifeIsCompleted()
        {
            if (_currentLife == MaxLife) return true;
            else return false;
        }

        public bool IsAlive()
        {
            return _currentLife > 0;
        }
    }
}