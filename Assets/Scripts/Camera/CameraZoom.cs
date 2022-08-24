using UnityEngine;
using UnityEngine.Serialization;

namespace ZombieDiorama.Cameras
{
    public class CameraZoom : MonoBehaviour
    {
        [FormerlySerializedAs("cameraController")] public CameraController CameraController;
        public float ZoomSpeed = 5;
        public Vector2 ZoomRange = new Vector2(-25, -50);

        void LateUpdate()
        {
            if (!CameraController) return;

            CameraController.CameraDistance += Input.mouseScrollDelta.y * ZoomSpeed * Time.deltaTime;

            CameraController.CameraDistance = Mathf.Clamp(CameraController.CameraDistance, ZoomRange.x, ZoomRange.y);
        }
    }
}