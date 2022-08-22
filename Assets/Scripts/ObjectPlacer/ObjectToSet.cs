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

        private GameObject _whoWillSet;
        private bool _alreadySelected;

        public ObjectToSet(Transform transform, SettableObjectInfo info)
        {
            TargetPosition = transform.position;
            TargetRotation = transform.eulerAngles;
            ObjectInfo = info;
        }

        public bool CanGet(GameObject gameObject = null)
        {
            if (!_whoWillSet) return true;

            if (_whoWillSet.activeSelf)
            {
                if (gameObject)
                    if (gameObject.Equals(_whoWillSet)) return true;

                if (!_alreadySelected) return true;
            }
            return false;
        }

        public void Select(GameObject whoSelected)
        {
            _whoWillSet = whoSelected;
            _alreadySelected = true;
        }

        public bool AlreadySelect(GameObject whoSelected)
        {
            if (!_whoWillSet) return false;
            if (_whoWillSet.Equals(whoSelected))
                return true;
            else
                return false;
        }

        public bool InvalidSelection()
        {
            if (!_alreadySelected) return false;
            if (!_whoWillSet) return true;
            if (!_whoWillSet.activeSelf) return true;
            return false;
        }

        public void UnSelect()
        {
            _whoWillSet = null;
            _alreadySelected = false;
        }
    }
}