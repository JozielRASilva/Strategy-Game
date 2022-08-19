using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieDiorama.Level.Coins
{
    public class CoinAdder : MonoBehaviour
    {
        public void AddCoins()
        {
            CoinCounter.Instance.AddCoin();
        }
    }
}