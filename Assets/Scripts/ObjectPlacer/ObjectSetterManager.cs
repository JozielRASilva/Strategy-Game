using System.Collections;
using System.Collections.Generic;
using MonsterLove.Pooller;
using UnityEngine;

namespace ZombieDiorama.ObjectPlacer
{
    public class ObjectSetterManager : MonoBehaviour
    {
        public List<SettableObjectInfo> ObjectsAvaliableToSet = new List<SettableObjectInfo>();
        public int poolSize = 20;

        public static ObjectSetterManager Instance;

        public List<ManageObject> objectsToSet = new List<ManageObject>();

        private void Awake()
        {
            Instance = this;
            foreach (var objectAvaliable in ObjectsAvaliableToSet)
            {
                PoolManager.WarmPool(objectAvaliable.ObjectToSet.gameObject, poolSize);
                PoolManager.WarmPool(objectAvaliable.ObjectPreviewChecker.gameObject, poolSize);
            }
            objectsToSet = new List<ManageObject>();
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

            objectsToSet.Add(manageObject);
        }

        public void SetObject(int id)
        {
            if (objectsToSet.Count <= id) return;

            SetObject(objectsToSet[id].objectToSet);
        }

        public void SetObject(ObjectToSet info)
        {
            ManageObject manageObject = objectsToSet.Find(o => o.objectToSet.Equals(info));
            if (manageObject == null) return;

            ObjectToSet objectTo = manageObject.objectToSet;
            PoolManager.SpawnObject(objectTo.ObjectInfo.ObjectToSet.gameObject, objectTo.position, Quaternion.Euler(objectTo.rotation));

            manageObject.settableObjectPreview.DisableObject();
            objectsToSet.Remove(manageObject);
        }

        public void GetObjectCheck(GameObject whoSelect)
        {
            ObjectToSet value = GetObjectToSet(whoSelect);

            if (value != null) Debug.Log($"Can select {whoSelect.name}");
            else Debug.Log($"Can not select {whoSelect.name}");
        }

        public ObjectToSet GetObjectToSet(GameObject whoSelect)
        {
            bool alreadySelect = objectsToSet.Exists(x => x.objectToSet.AlreadySelect(whoSelect));
            if (alreadySelect)
                return objectsToSet.Find(x => x.objectToSet.AlreadySelect(whoSelect)).objectToSet;
            foreach (var objectTo in objectsToSet)
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
            bool alreadySelect = objectsToSet.Exists(x => x.objectToSet.AlreadySelect(whoSelect));
            if (alreadySelect)
                return objectsToSet.Find(x => x.objectToSet.AlreadySelect(whoSelect)).settableObjectPreview.transform;
            return null;
        }

        private void CheckObjectsToSet()
        {
            foreach (var objectTo in objectsToSet)
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
            foreach (var objectTo in objectsToSet)
            {
                if (!objectTo.settableObjectPreview)
                    objectTo.settableObjectPreview = GetPreviewObject(objectTo.objectToSet.ObjectInfo);
                if (objectTo.settableObjectPreview)
                    objectTo.settableObjectPreview.ShowSelected(objectTo.objectToSet.position, objectTo.objectToSet.rotation);
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