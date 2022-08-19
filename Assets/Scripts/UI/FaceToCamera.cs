using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieDiorama.UI
{
    public class FaceToCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}