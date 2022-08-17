using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DestroyOnTouch : KillableObject
{
    [Title("Destroy Info")]
    public LayerMask Destructor;

    private void OnTriggerEnter(Collider other)
    {
        if (UnityLayerMaskExtensions.Contains(Destructor, other.gameObject.layer))
        {
            Destroy();
        }
    }
}
