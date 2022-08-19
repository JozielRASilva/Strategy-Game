using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ZombieDiorama.Character;

namespace ZombieDiorama.UI
{
    public class LifeBar : MonoBehaviour
    {
        public Health health;

        public Image FillBar;

        public UnityEvent OnBecomeLifeFull;
        public UnityEvent OnNotLifeFull;

        private bool becomeFull = true;
        private bool died = false;

        private void Update()
        {
            if (!health) return;

            if (health.GetLife() != health.maxLife)
            {
                if (becomeFull)
                {
                    OnNotLifeFull?.Invoke();
                    becomeFull = false;
                }

                if (!health.IsAlive() && !died)
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
            if (!health) return;
            float maxLife = health.maxLife;
            float currentLife = health.GetLife();
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