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
        public Health Health;

        public Image FillBar;

        public UnityEvent OnBecomeLifeFull;
        public UnityEvent OnNotLifeFull;

        private bool _becomeFull = true;
        private bool _died = false;

        // TODO: Colocar em um action para não executar direto
        private void Update()
        {
            if (!Health) return;

            if (Health.GetLife() != Health.maxLife)
            {
                if (_becomeFull)
                {
                    OnNotLifeFull?.Invoke();
                    _becomeFull = false;
                }

                if (!Health.IsAlive() && !_died)
                {

                    OnBecomeLifeFull?.Invoke();
                    _died = true;
                }
            }
            else
            {
                if (!_becomeFull)
                {
                    OnBecomeLifeFull?.Invoke();
                    _becomeFull = true;
                }
            }

            UpdateLifeBar();
        }

        private void UpdateLifeBar()
        {
            if (!Health) return;
            float maxLife = Health.maxLife;
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