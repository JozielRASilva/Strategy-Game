using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieDiorama.Level.Store
{
    public class StoreRefundCall : MonoBehaviour
    {
        public void Refund()
        {
            StoreManager.RefundCall();
        }
    }
}
