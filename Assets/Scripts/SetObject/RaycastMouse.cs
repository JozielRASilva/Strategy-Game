using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMouse : MonoBehaviour
{
    public Camera _camera;

    private Vector3 currentPoint;

    void Start()
    {
        if (!_camera)
            _camera = Camera.main;
    }

    public Vector3 GetPosition(LayerMask mask)
    {

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            Vector3 objectHit = hit.point;

            return objectHit;

        }

        return Vector2.zero;
    }

    public bool ValidPosition(LayerMask mask)
    {

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            return true;

        }

        return false;
    }

}
