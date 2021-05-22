using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    public void Open(string value)
    {
        Application.OpenURL(value);
    }
}
