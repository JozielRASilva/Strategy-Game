using UnityEngine;
using UnityEditor;
using ZombieDiorama.Character.AIs;

#if UNITY_EDITOR
[CustomEditor(typeof(Zombie))]
public class FieldsEditor : Editor
{
    private void OnSceneGUI()
    {
        Zombie zombie = (Zombie)target;

        //Field of View
        Handles.color = Color.red;
        Handles.DrawWireArc(zombie.transform.position, Vector3.up, Vector3.forward, 360, zombie.ZombieAttributes.DistanceToTarget);
        zombie.ZombieAttributes.DistanceToTarget = (float)Handles.ScaleValueHandle(zombie.ZombieAttributes.DistanceToTarget, zombie.transform.position + zombie.transform.forward * zombie.ZombieAttributes.DistanceToTarget, zombie.transform.rotation, 1, Handles.ConeHandleCap, 1);

        //Listening Field
        Handles.color = Color.green;
        Handles.DrawWireArc(zombie.transform.position, Vector3.up, Vector3.forward, 360, zombie.ZombieAttributes.ListeningField);
        zombie.ZombieAttributes.ListeningField = (float)Handles.ScaleValueHandle(zombie.ZombieAttributes.ListeningField, zombie.transform.position + zombie.transform.forward * zombie.ZombieAttributes.ListeningField, zombie.transform.rotation, 1, Handles.ConeHandleCap, 1);
    }

}
#endif
