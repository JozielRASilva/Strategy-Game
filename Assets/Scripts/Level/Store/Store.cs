using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Store : MonoBehaviour
{

    public static Store Instance;

    public List<StoreItem> storeItems = new List<StoreItem>();

    [Title("Events")]
    public EventItem OnBuy;
    public UnityEvent OnCanBuy;
    public UnityEvent OnCanNotBuy;
    public UnityEvent OnRefund;

    private StoreItem _currentBought;


    private void Awake()
    {
        Instance = this;
    }

    public bool CanBuy(int itemId)
    {
        if (storeItems.Count == 0) return false;
        if (!Coins.Instance || itemId >= storeItems.Count) return false;

        StoreItem item = storeItems[itemId];

        if (Coins.Instance.CoinsCount - item.price >= 0)
        {
            return true;
        }

        return false;
    }

    public void Refund()
    {
        if (_currentBought == null) return;

        Coins.Instance.AddCoin(_currentBought.price);
        OnRefund?.Invoke();
        _currentBought = null;
    }

    public void Buy(int itemId)
    {
        if (CanBuy(itemId))
        {
            StoreItem item = storeItems[itemId];

            OnCanBuy?.Invoke();

            OnBuy?.Invoke(item.Item);

            Coins.Instance.AddCoin(-item.price);

            _currentBought = item;
        }
        else
        {

            OnCanNotBuy?.Invoke();

        }
    }

}
