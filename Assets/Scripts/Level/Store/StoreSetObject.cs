using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Level.Store
{
    [RequireComponent(typeof(StoreManager))]
    public class StoreSetObject : MonoBehaviour
    {
        private void Awake()
        {
            StoreManager.OnBuyAction += StartSetObject;
        }

        private void OnDestroy()
        {
            StoreManager.OnBuyAction -= StartSetObject;
        }

        private void StartSetObject(SettableObjectInfo settableObject)
        {
            ObjectSetterIndicator.IndicateObject(settableObject);
        }
    }
}