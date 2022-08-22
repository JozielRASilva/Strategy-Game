using System.Collections;
using System.Collections.Generic;
using MonsterLove.Pooller;
using UnityEngine;
using ZombieDiorama.Utilities.Patterns;

namespace ZombieDiorama.ObjectPlacer
{
    public class ObjectSetterManager : Singleton<ObjectSetterManager>
    {
        public List<SettableObjectInfo> ObjectsAvaliableToSet = new List<SettableObjectInfo>();
        public int PoolSize = 20;
        public List<ManageObject> ObjectsToSet = new List<ManageObject>();

        protected override void Awake()
        {
            base.Awake();
            foreach (var objectAvaliable in ObjectsAvaliableToSet)
            {
                PoolManager.WarmPool(objectAvaliable.ObjectToSet.gameObject, PoolSize);
                PoolManager.WarmPool(objectAvaliable.ObjectPreviewChecker.gameObject, PoolSize);
            }
            ObjectsToSet = new List<ManageObject>();
        }

        public SettableObjectPreview GetPreviewObject(SettableObjectInfo info)
        {
            SettableObjectPreview preview = PoolManager.SpawnObject(info.ObjectPreviewChecker.gameObject)?.GetComponent<SettableObjectPreview>();
            return preview;
        }

        private void Update()
        {
            CheckObjectsToSet();
            ShowObjectsWaiting();
        }

        public void AddObjectToSet(Transform transform, SettableObjectInfo info)
        {
            ObjectToSet objectTo = new ObjectToSet(transform, info);
            ManageObject manageObject = new ManageObject(objectTo);

            ObjectsToSet.Add(manageObject);
        }

        public void SetObject(int id)
        {
            if (ObjectsToSet.Count <= id) return;

            SetObject(ObjectsToSet[id].objectToSet);
        }

        public void SetObject(ObjectToSet info)
        {
            ManageObject manageObject = ObjectsToSet.Find(o => o.objectToSet.Equals(info));
            if (manageObject == null) return;

            ObjectToSet objectTo = manageObject.objectToSet;
            PoolManager.SpawnObject(objectTo.ObjectInfo.ObjectToSet.gameObject, objectTo.TargetPosition, Quaternion.Euler(objectTo.TargetRotation));

            manageObject.settableObjectPreview.DisableObject();
            ObjectsToSet.Remove(manageObject);
        }

        public void GetObjectCheck(GameObject whoSelect)
        {
            ObjectToSet value = GetObjectToSet(whoSelect);

            if (value != null) Debug.Log($"Can select {whoSelect.name}");
            else Debug.Log($"Can not select {whoSelect.name}");
        }

        public ObjectToSet GetObjectToSet(GameObject whoSelect)
        {
            bool alreadySelect = ObjectsToSet.Exists(x => x.objectToSet.AlreadySelect(whoSelect));
            if (alreadySelect)
                return ObjectsToSet.Find(x => x.objectToSet.AlreadySelect(whoSelect)).objectToSet;
            foreach (var objectTo in ObjectsToSet)
            {
                if (objectTo.objectToSet.CanGet())
                {
                    objectTo.objectToSet.Select(whoSelect);
                    return objectTo.objectToSet;
                }
            }
            return null;
        }

        public Transform GetObjectReference(GameObject whoSelect)
        {
            bool alreadySelect = ObjectsToSet.Exists(x => x.objectToSet.AlreadySelect(whoSelect));
            if (alreadySelect)
                return ObjectsToSet.Find(x => x.objectToSet.AlreadySelect(whoSelect)).settableObjectPreview.transform;
            return null;
        }

        private void CheckObjectsToSet()
        {
            foreach (var objectTo in ObjectsToSet)
            {
                if (objectTo.objectToSet.InvalidSelection())
                {
                    if (objectTo.settableObjectPreview)
                        objectTo.settableObjectPreview.DisableObject();

                    objectTo.objectToSet.UnSelect();
                }
            }
        }

        private void ShowObjectsWaiting()
        {
            foreach (var objectTo in ObjectsToSet)
            {
                if (!objectTo.settableObjectPreview)
                    objectTo.settableObjectPreview = GetPreviewObject(objectTo.objectToSet.ObjectInfo);
                if (objectTo.settableObjectPreview)
                    objectTo.settableObjectPreview.ShowSelected(objectTo.objectToSet.TargetPosition, objectTo.objectToSet.TargetRotation);
            }
        }

        [System.Serializable]
        public class ManageObject
        {
            public ObjectToSet objectToSet;
            public SettableObjectPreview settableObjectPreview;

            public ManageObject(ObjectToSet _objectToSet)
            {
                objectToSet = _objectToSet;
            }
        }
    }
}