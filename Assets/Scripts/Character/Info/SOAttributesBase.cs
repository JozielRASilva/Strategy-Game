using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using ZombieDiorama.Utilities.Primitives;

namespace ZombieDiorama.Character.Info
{
    public abstract class SOAttributesBase : ScriptableObject
    {   
        [TitleGroup("Movement")]
        public float Speed = 6;

        [TitleGroup("Combat")]
        public SOString TargetTag;
        public float DistanceToTarget;
    }
}