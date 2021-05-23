using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public ControleCamera cameraController;
    public float ZoomSpeed = 5;
    public Vector2 ZoomRange = new Vector2(-25, -50);

    void LateUpdate()
    {
        if (!cameraController) return;

        cameraController.camdistancia += Input.mouseScrollDelta.y * ZoomSpeed * Time.deltaTime;

        cameraController.camdistancia = Mathf.Clamp(cameraController.camdistancia, ZoomRange.x, ZoomRange.y);
    }
}
