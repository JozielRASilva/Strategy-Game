using UnityEngine;
using System;
using Sirenix.OdinInspector;

[System.Serializable]
public class ObjectToSet
{


    public Vector3 position;
    public Vector3 rotation;

    [Title("Private values")]

    public SettableObjectInfo ObjectInfo;
    [SerializeField]
    private GameObject whoWillSet;
    [SerializeField]
    private bool alreadySelected;

    public ObjectToSet(Transform transform, SettableObjectInfo info)
    {

        position = transform.position;
        rotation = transform.eulerAngles;
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
        if(!whoWillSet) return false;
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