using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using ZombieDiorama.Utilities.Patterns;

namespace ZombieDiorama.Level.Coins
{
    // TODO: Trocar os singletons e armazenar
    public class CoinCounter : Singleton<CoinCounter>
    {
        [FormerlySerializedAs("moedaTxt")] public Text CoinText;
        [FormerlySerializedAs("moeda")] public int CoinsCount; //TODO: Colocar scriptable object
        [FormerlySerializedAs("valorMoeda")] public int CoinValue = 1; //TODO: Trocar nome par um mais claro e colocar como scriptable object  

        public UnityEvent OnGetCash;

        private void Start()
        {
            CoinText.text = CoinsCount.ToString();
        }

        public void AddCoin()
        {
            AddCoin(CoinValue);
        }

        public void AddCoin(int value)
        {
            CoinsCount += value;
            CoinText.text = CoinsCount.ToString();
            OnGetCash?.Invoke();
        }
    }
}