using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public CameraController cameraController;
    public float ZoomSpeed = 5;
    public Vector2 ZoomRange = new Vector2(-25, -50);

    void LateUpdate()
    {
        if (!cameraController) return;

        cameraController.CameraDistance += Input.mouseScrollDelta.y * ZoomSpeed * Time.deltaTime;

        cameraController.CameraDistance = Mathf.Clamp(cameraController.CameraDistance, ZoomRange.x, ZoomRange.y);
    }
}
