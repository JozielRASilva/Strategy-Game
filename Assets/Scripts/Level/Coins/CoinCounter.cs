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
        [SerializeField] private SOInt Counter;
        [SerializeField] private SOInt InitialValue;

        public static Action<int> OnUpdateCounter;

        private void Start()
        {
            SetCoin(InitialValue.Value);
        }

        public static int GetValue()
        {
            if (!Instance)
                return 0;

            if (!Instance.Counter)
                return 0;

            return Instance.Counter.Value;
        }

        public static void Add(int value)
        {
            Instance.AddCoin(value);
        }

        private void AddCoin(int value)
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