using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetter : MonoBehaviour
{

    public SettableObject settable;

    public RaycastMouse raycastMouse;

    public LayerMask WhereCanSet;

    public Vector3 positionOffset = new Vector3(0, 0.2f, 0);

    private SettableObject current;

    public Vector3 rotateValue;

    private void Update()
    {

        if (!settable || !raycastMouse) return;

        Vector3 point = raycastMouse.GetPosition(WhereCanSet);

        if (!current) current = Instantiate(settable); // Change to pool
        current.gameObject.SetActive(true);
        current?.ShowPreview(point + positionOffset);


        current?.Rotate(rotateValue * Input.GetAxis("Horizontal"));


    }

}
