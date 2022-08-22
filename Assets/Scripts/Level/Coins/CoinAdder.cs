using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Utilities.Primitives;

namespace ZombieDiorama.Level.Coins
{
    public class CoinAdder : MonoBehaviour
    {
        public SOInt Amount;

        public void AddCoins()
        {
            if (Amount)
                CoinCounter.Instance.AddCoin(Amount.Value);
        }
    }
}