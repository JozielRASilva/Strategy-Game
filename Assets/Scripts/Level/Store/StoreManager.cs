using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using ZombieDiorama.Level.Coins;
using ZombieDiorama.Utilities.Patterns;
using System;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Level.Store
{
    public class StoreManager : Singleton<StoreManager>
    {
        public List<StoreItem> storeItems = new List<StoreItem>();

        [Title("Events")]
        public EventItem OnBuy;
        public UnityEvent OnCanBuy;
        public UnityEvent OnCanNotBuy;
        public UnityEvent OnRefund;

        public static Action<SettableObjectInfo> OnBuyAction;
        public static Action OnCanBuyAction;
        public static Action OnCanNotBuyAction;
        public static Action OnRefundAction;

        private StoreItem currentBought;

        private bool CanBuy(int itemId)
        {
            if (storeItems.Count == 0) return false;
            if (itemId >= storeItems.Count) return false;

            StoreItem item = storeItems[itemId];

            if (CoinCounter.GetValue() - item.Price >= 0)
            {
                return true;
            }
            return false;
        }
        public static void RefundCall()
        {
            Instance?.Refund();
        }

        public static void BuyCall(int itemId)
        {
            Instance?.Buy(itemId);
        }

        public void Refund()
        {
            if (currentBought == null) return;

            CoinCounter.Add(currentBought.Price);
            OnRefund?.Invoke();
            OnRefundAction?.Invoke();
            currentBought = null;
        }

        public void Buy(int itemId)
        {
            if (CanBuy(itemId))
            {
                StoreItem item = storeItems[itemId];

                OnCanBuy?.Invoke();
                OnBuy?.Invoke(item.Item);

                OnCanBuyAction?.Invoke();
                OnBuyAction?.Invoke(item.Item);

                CoinCounter.Add(-item.Price);
                currentBought = item;
            }
            else
            {
                OnCanNotBuy?.Invoke();
                OnCanNotBuyAction?.Invoke();
            }
        }
    }
}