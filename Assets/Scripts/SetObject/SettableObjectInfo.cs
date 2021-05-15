using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectInfo", menuName = "ObjectInfo", order = 1)]
public class SettableObjectInfo : ScriptableObject
{

    public string ObjectName = "SettableObject";

    public string instanceID;

    public GameObject ObjectToSet;

    public SettableObjectPreview ObjectPreviewChecker;

    private void OnValidate()
    {
        instanceID = this.GetInstanceID().ToString();
    }

}
