using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System;
using MonsterLove.Pooller;

namespace ZombieDiorama.Character
{
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
        public Action ActionOnKill;

        public bool HasDelayToDestroy = false;
        [ShowIf("HasDelayToDestroy", true)]
        public float delayToDestroy = 0.2f;

        private string defaultTag;

        protected virtual void Awake()
        {
            defaultTag = gameObject.tag;
        }

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
            ActionOnKill?.Invoke();

            if (HasDelayToDestroy)
                StartCoroutine(DestroyCO());
            else Kill();
        }

        public IEnumerator DestroyCO()
        {
            gameObject.tag = "Untagged";
            yield return new WaitForSeconds(delayToDestroy);
            gameObject.tag = defaultTag;
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
}