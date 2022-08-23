using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZombieDiorama.Level.Coins
{
    public class CoinDisplayer : MonoBehaviour
    {
        public Text CoinText;
        public UnityEvent OnUpdate;

        private void Awake()
        {
            CoinCounter.OnUpdateCounter += UpdateCoin;
        }

        public void UpdateCoin(int value)
        {
            CoinText.text = value.ToString();
            OnUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            CoinCounter.OnUpdateCounter -= UpdateCoin;
        }

        private void OnDisable()
        {
            CoinCounter.OnUpdateCounter -= UpdateCoin;
        }
    }
}