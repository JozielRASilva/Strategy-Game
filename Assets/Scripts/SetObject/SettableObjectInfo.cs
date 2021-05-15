using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettableObjectInfo : ScriptableObject
{

    public string ObjectName = "SttableObject";

    public string instanceID;

    public GameObject ObjectToSet;

    public SettableObjectPreview ObjectPreviewChecker;

    private void OnValidate()
    {
        instanceID = this.GetInstanceID().ToString();
    }

}
