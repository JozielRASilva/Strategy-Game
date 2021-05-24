using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Zombie))]
public class FieldsEditor : Editor
{
    private void OnSceneGUI()
    {
        Zombie zombie = (Zombie)target;

        //Field of View
        Handles.color = Color.red;
        Handles.DrawWireArc(zombie.transform.position, Vector3.up, Vector3.forward, 360, zombie.distanceView);
        zombie.distanceView = (float)Handles.ScaleValueHandle(zombie.distanceView, zombie.transform.position + zombie.transform.forward * zombie.distanceView, zombie.transform.rotation, 1, Handles.ConeHandleCap, 1);

        //Listening Field
        Handles.color = Color.green;
        Handles.DrawWireArc(zombie.transform.position, Vector3.up, Vector3.forward, 360, zombie.listeningField);
        zombie.listeningField = (float)Handles.ScaleValueHandle(zombie.listeningField, zombie.transform.position + zombie.transform.forward * zombie.listeningField, zombie.transform.rotation, 1, Handles.ConeHandleCap, 1);
    }

}
