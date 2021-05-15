using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            PoolManager.WarmPool(objectAvaliable.ObjectToSet, poolSize);
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

    public ObjectToSet GetObjectToSet(GameObject whoSelect)
    {
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
