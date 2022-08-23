using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieDiorama.UI
{
    public abstract class UIFractionCounterDisplayer : MonoBehaviour
    {
        public Text Total;
        public Text Current;

        protected abstract void Subscribe();
        protected abstract void UnSubscribe();

        private void Awake()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }

        protected virtual void UpdateTotal(int value)
        {
            if (Total)
                Total.text = value.ToString();

            UpdateCurrent(value);
        }

        protected virtual void UpdateCurrent(int value)
        {
            if (Current)
                Current.text = value.ToString();
        }
    }
}