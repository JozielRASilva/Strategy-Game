using ZombieDiorama.ObjectPlacer;
using UnityEngine.Serialization;

namespace ZombieDiorama.Level.Store
{
    [System.Serializable]
    public class StoreItem
    {
        public SettableObjectInfo Item;
       [FormerlySerializedAs("price")] public int Price = 1;
    }
}