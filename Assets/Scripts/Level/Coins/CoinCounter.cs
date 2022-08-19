using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ZombieDiorama.Level.Coins
{
    public class CoinCounter : MonoBehaviour
    {
        [FormerlySerializedAs("moedaTxt")] public Text CoinText;
        [FormerlySerializedAs("moeda")] public int CoinsCount;
        [FormerlySerializedAs("valorMoeda")] public int CoinValue = 1;
        [FormerlySerializedAs("moedas")] public static CoinCounter Instance;

        public UnityEvent OnGetCash;

        void Start()
        {
            Instance = this;
            CoinText.text = CoinsCount.ToString();
        }

        public void AddCoin()
        {
            CoinsCount += CoinValue;
            CoinText.text = CoinsCount.ToString();
            OnGetCash?.Invoke();
        }

        public void AddCoin(int value)
        {
            CoinsCount += value;
            CoinText.text = CoinsCount.ToString();
            OnGetCash?.Invoke();
        }
    }
}