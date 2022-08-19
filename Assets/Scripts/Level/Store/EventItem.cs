using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using ZombieDiorama.ObjectPlacer;

namespace ZombieDiorama.Level.Store
{
    [System.Serializable]
    public class EventItem : UnityEvent<SettableObjectInfo>
    {

    }
}