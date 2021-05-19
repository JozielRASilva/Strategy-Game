using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class EventTriggerArea : MonoBehaviour
{

    public LayerMask LayerToDetect;

    [TitleGroup("On Trigger Enter")]
    public UnityEvent OnEnter;
    public UnityEvent OnStay;
    public UnityEvent OnExit;


    private void OnTriggerEnter(Collider other)
    {
        if (LayerContains(other.transform.gameObject.layer, LayerToDetect))
        {  
            Debug.Log($"Enter {other.name}");
            OnEnter?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (LayerContains(other.transform.gameObject.layer, LayerToDetect))
        {
            Debug.Log($"Stay {other.name}");
            OnStay?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerContains(other.transform.gameObject.layer, LayerToDetect))
        {
            Debug.Log($"Exit {other.name}");
            OnExit?.Invoke();
        }
    }


    private bool LayerContains(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}