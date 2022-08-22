using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ZombieDiorama.Level.Coins
{
    // TODO: Trocar os singletons e armazenar
    public class CoinCounter : MonoBehaviour
    {
        [FormerlySerializedAs("moedaTxt")] public Text CoinText;
        [FormerlySerializedAs("moeda")] public int CoinsCount; //TODO: Colocar scriptable object
        [FormerlySerializedAs("valorMoeda")] public int CoinValue = 1; //TODO: Trocar nome par um mais claro e colocar como scriptable object  
        [FormerlySerializedAs("moedas")] public static CoinCounter Instance;

        public UnityEvent OnGetCash;

        private void Start()
        {
            Instance = this;
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