using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;


public class KillableObject : MonoBehaviour, IKillableObject
{




    [Title("Destroy")]
    public GameObject MainBody;

    public UnityEvent OnKill;

    public bool HasDelayToDestroy = false;
    [ShowIf("HasDelayToDestroy", true)]
    public float delayToDestroy = 0.2f;

    public void Destroy()
    {
        OnKill?.Invoke();

        if (HasDelayToDestroy)
            StartCoroutine(DestroyCO());
        else Kill();
    }

    public IEnumerator DestroyCO()
    {

        yield return new WaitForSeconds(delayToDestroy);

        Kill();
    }

    public void Kill()
    {
        if (MainBody)
            MainBody.SetActive(false);
        else
            gameObject.SetActive(false);
    }
}