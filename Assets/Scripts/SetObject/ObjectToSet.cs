using UnityEngine;
using System;
using Sirenix.OdinInspector;

[System.Serializable]
public class ObjectToSet
{


    public Vector3 position;
    public Vector3 rotation;

    [Title("Private values")]
    [SerializeField]
    private SettableObjectInfo ObjectInfo;
    [SerializeField]
    private GameObject whoWillSet;
    [SerializeField]
    private bool alreadySelected;

    public ObjectToSet(Transform transform)
    {

        position = transform.position;
        rotation = transform.eulerAngles;
    }

    public bool CanGet()
    {
        if (!whoWillSet) return true;

        if (whoWillSet.activeSelf)
        {
            if (!alreadySelected) return true;
        }

        return false;
    }

    public void Select(GameObject whoSelected)
    {
        whoWillSet = whoSelected;
        alreadySelected = true;
    }

    public bool InvalidSelection()
    {
        if(!alreadySelected) return false;

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