using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieDiorama.Level
{
    public class OpenUrl : MonoBehaviour
    {
        public void Open(string value)
        {
            Application.OpenURL(value);
        }
    }
}