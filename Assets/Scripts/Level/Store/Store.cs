using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using ZombieDiorama.Level.Coins;
using ZombieDiorama.Utilities.Patterns;

namespace ZombieDiorama.Level.Store
{
    public class Store : Singleton<Store>
    {
        public List<StoreItem> storeItems = new List<StoreItem>();

        [Title("Events")]
        public EventItem OnBuy;
        public UnityEvent OnCanBuy;
        public UnityEvent OnCanNotBuy;
        public UnityEvent OnRefund;

        private StoreItem currentBought;

        public bool CanBuy(int itemId)
        {
            if (storeItems.Count == 0) return false;
            if (itemId >= storeItems.Count) return false;

            StoreItem item = storeItems[itemId];

            if (CoinCounter.GetValue() - item.price >= 0)
            {
                return true;
            }
            return false;
        }

        public void Refund()
        {
            if (currentBought == null) return;

            CoinCounter.Add(currentBought.price);
            OnRefund?.Invoke();
            currentBought = null;
        }

        public void Buy(int itemId)
        {
            if (CanBuy(itemId))
            {
                StoreItem item = storeItems[itemId];

                OnCanBuy?.Invoke();
                OnBuy?.Invoke(item.Item);
                CoinCounter.Add(-item.price);
                currentBought = item;
            }
            else
            {
                OnCanNotBuy?.Invoke();
            }
        }
    }
}