using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Level.Store
{
    [System.Serializable]
    public class StoreItem
    {
        public SettableObjectInfo Item;
        public int price = 1;
    }
}