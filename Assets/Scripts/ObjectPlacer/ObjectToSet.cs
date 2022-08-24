using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace ZombieDiorama.ObjectPlacer
{
    [System.Serializable]
    public class ObjectToSet
    {
        [FormerlySerializedAs("position")] public Vector3 TargetPosition;
        [FormerlySerializedAs("rotation")] public Vector3 TargetRotation;

        [Title("Private values")]

        public SettableObjectInfo ObjectInfo;
        [SerializeField]
        private GameObject whoWillSet;
        [SerializeField]
        private bool alreadySelected;

        public ObjectToSet(Transform transform, SettableObjectInfo info)
        {
            TargetPosition = transform.position;
            TargetRotation = transform.eulerAngles;
            ObjectInfo = info;
        }

        public bool CanGet(GameObject gameObject = null)
        {
            if (!whoWillSet) return true;

            if (whoWillSet.activeSelf)
            {
                if (gameObject)
                    if (gameObject.Equals(whoWillSet)) return true;

                if (!alreadySelected) return true;
            }
            return false;
        }

        public void Select(GameObject whoSelected)
        {
            whoWillSet = whoSelected;
            alreadySelected = true;
        }

        public bool AlreadySelect(GameObject whoSelected)
        {
            if (!whoWillSet) return false;
            if (whoWillSet.Equals(whoSelected))
                return true;
            else
                return false;
        }

        public bool InvalidSelection()
        {
            if (!alreadySelected) return false;
            if (!whoWillSet) return true;
            if (!whoWillSet.activeSelf) return true;
            return false;
        }

        public void UnSelect()
        {
            whoWillSet = null;
            alreadySelected = false;
        }
    }
}