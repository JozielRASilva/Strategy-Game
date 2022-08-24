using UnityEngine;

namespace ZombieDiorama.Utilities
{
    public class TargetFrameRate : MonoBehaviour
    {
        public int FrameRate = 60;

        protected void Awake()
        {
            Application.targetFrameRate = FrameRate;
        }
    }
}