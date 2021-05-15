using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetterManager : MonoBehaviour
{

    public static ObjectSetterManager Instance;

    [SerializeField]
    private List<ObjectToSet> objectsToSet = new List<ObjectToSet>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckObjectsToSet();
    }

    public void AddObjectToSet(Transform transform)
    {
        ObjectToSet objectTo = new ObjectToSet(transform);

        objectsToSet.Add(objectTo);

    }

    public ObjectToSet GetObjectToSet(GameObject whoSelect)
    {
        foreach (var objectTo in objectsToSet)
        {
            if (objectTo.CanGet())
            {
                objectTo.Select(whoSelect);

                return objectTo;
            }
        }

        return null;
    }

    private void CheckObjectsToSet()
    {
        foreach (var objectTo in objectsToSet)
        {
            if (objectTo.InvalidSelection())
            {
                objectTo.UnSelect();
            }
        }
    }



}
