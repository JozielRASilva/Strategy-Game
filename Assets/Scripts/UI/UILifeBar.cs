using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ZombieDiorama.Character;
using UnityEngine.Serialization;

namespace ZombieDiorama.UI
{
    public class UILifeBar : MonoBehaviour
    {
        [FormerlySerializedAs("health")] public Health Health;

        public Image FillBar;

        public UnityEvent OnBecomeLifeFull;
        public UnityEvent OnNotLifeFull;

        private bool becomeFull = true;
        private bool died = false;

        private void Start()
        {
            if (Health)
                Health.OnChange += UpdateBar;
        }

        private void UpdateBar()
        {
            if (!Health) return;

            if (Health.GetLife() != Health.MaxLife)
            {
                if (becomeFull)
                {
                    OnNotLifeFull?.Invoke();
                    becomeFull = false;
                }

                if (!Health.IsAlive() && !died)
                {
                    OnBecomeLifeFull?.Invoke();
                    died = true;
                }
            }
            else
            {
                if (!becomeFull)
                {
                    OnBecomeLifeFull?.Invoke();
                    becomeFull = true;
                }
            }
            UpdateLifeBar();
        }

        private void UpdateLifeBar()
        {
            float maxLife = Health.MaxLife;
            float currentLife = Health.GetLife();
            float value = Remap(currentLife, 0, maxLife, 0, 1);

            if (FillBar)
                FillBar.fillAmount = value;
        }

        public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs = from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;
            return to;
        }
    }
}