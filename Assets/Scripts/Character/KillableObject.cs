using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System;


public class KillableObject : MonoBehaviour, IKillableObject
{

    public UnityEvent EventOnRespawn;
    public Action OnRespawn;

    public UnityEvent EventOnDisable;
    public Action ActionOnDisable;

    public bool FromPool;

    [Title("Destroy")]
    public bool MainBodyIsOther = false;
    [ShowIf("MainBodyIsOther", true)]
    public GameObject MainBody;

    public UnityEvent OnKill;

    public bool HasDelayToDestroy = false;
    [ShowIf("HasDelayToDestroy", true)]
    public float delayToDestroy = 0.2f;

    private void OnEnable()
    {
        EventOnRespawn?.Invoke();

        OnRespawn?.Invoke();
    }

    private void OnDisable()
    {
        EventOnDisable?.Invoke();

        ActionOnDisable?.Invoke();
    }

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
        {
            if (FromPool)
                PoolManager.ReleaseObject(MainBody);
            else
                MainBody.SetActive(false);
        }
        else
        {
            if (FromPool)
                PoolManager.ReleaseObject(this.gameObject);
            else
                gameObject.SetActive(false);
        }
    }
}