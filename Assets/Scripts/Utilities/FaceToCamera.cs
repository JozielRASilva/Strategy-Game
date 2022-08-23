using UnityEngine;

namespace ZombieDiorama.Utilities
{
    public class FaceToCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}