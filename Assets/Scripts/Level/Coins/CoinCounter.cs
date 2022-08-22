using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using ZombieDiorama.Utilities.Patterns;
using ZombieDiorama.Utilities.Primitives;
using System;

namespace ZombieDiorama.Level.Coins
{
    public class CoinCounter : Singleton<CoinCounter>
    {
        public SOInt Counter; 
        public SOInt InitialValue; 

        public static Action<int> OnUpdateCounter;

        private void Start()
        {
            SetCoin(InitialValue.Value);
        }

        public void AddCoin(int value)
        {
            SetCoin(Counter.Value + value);
        }

        private void SetCoin(int value)
        {
            Counter.Value = value;
            OnUpdateCounter.Invoke(Counter.Value);
        }
    }
}