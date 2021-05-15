using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastMouse))]
public class ObjectSetterIndicator : MonoBehaviour
{

    public SettableObjectInfo settable;

    public LayerMask WhereCanSet;

    public Vector3 positionOffset = new Vector3(0, 0.2f, 0);

    public Vector3 rotateValue;

    public RectTransform rectTransform;
    public Vector3 rotation = new Vector3(0, 0, 0);

    public bool startActivated = true;

    private bool activated = true;

    private RaycastMouse raycastMouse;
    private SettableObjectPreview current;

    private void Awake()
    {
        raycastMouse = GetComponent<RaycastMouse>();

        if (startActivated)
            EnableSetter();
        else
            DisableSetter();
    }

    private void Update()
    {
        if (!activated) return;

        if (!settable || !raycastMouse) return;

        Vector3 point = raycastMouse.GetPosition(WhereCanSet);

        PreviewObjectPosition(point);

        SetterActions();

        ShowCanvasFeedback(point);

    }

    private void SetterActions()
    {
        float horizontal = Input.GetAxis("Horizontal");
        current?.Rotate(rotateValue * horizontal);
    }

    private void ShowCanvasFeedback(Vector3 point)
    {
        if (!current)
        {
            HideCanvasFeedback();
            return;
        }
        else
        {
            if (!current.canRotate)
            {
                HideCanvasFeedback();
                return;
            }
        }

        rectTransform.gameObject.SetActive(true);

        rectTransform.transform.position = point + positionOffset;
        rectTransform.LookAt(Camera.main.transform);
        rectTransform.eulerAngles = new Vector3(90 + rotation.x, rectTransform.eulerAngles.y + rotation.y, rectTransform.eulerAngles.z + rotation.z);
    }

    private void HideCanvasFeedback()
    {
        rectTransform.gameObject.SetActive(false);
    }

    private void PreviewObjectPosition(Vector3 point)
    {

        if (!current) current = Instantiate(settable.ObjectPreviewChecker); // Change to pool

        current.gameObject.SetActive(true);
        current?.ShowPreview(point + positionOffset);

    }

    public void IndicateObjectToSet(SettableObjectInfo info)
    {
        settable = info;

        EnableSetter();
    }

    public void StopIndicateObjectToSet()
    {
        DisableSetter();
    }


    private void EnableSetter()
    {
        activated = true;

        current?.EnableObject();
    }

    private void DisableSetter()
    {
        activated = false;

        current?.DisableObject();
    }
}
