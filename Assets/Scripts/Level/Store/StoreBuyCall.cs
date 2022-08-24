using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZombieDiorama.Level.Store
{
    public class StoreBuyCall : MonoBehaviour
    {
        public int StoreItemsIndex;

        public void Buy()
        {
            StoreManager.Instance.Buy(StoreItemsIndex);
        }
    }
}
