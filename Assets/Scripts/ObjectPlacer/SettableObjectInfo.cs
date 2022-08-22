using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieDiorama.ObjectPlacer
{
    [CreateAssetMenu(fileName = "SO_ObjectInfo", menuName = "ObjectPlacer/ObjectInfo", order = 1)]
    public class SettableObjectInfo : ScriptableObject
    {
        public string ObjectName = "SettableObject";

        public GameObject ObjectToSet;
        public SettableObjectPreview ObjectPreviewChecker;
    }
}