using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Character.Behaviours;

namespace ZombieDiorama.Character.AIs
{
    public class AIBase : MonoBehaviour
    {
        protected TargetHandler targetHandler;
        protected NavMeshHandler navMeshHandler;
        protected BehaviourTree behaviourTree;

        #region  SETUP
        protected virtual void Awake()
        {
            targetHandler = GetComponent<TargetHandler>();
            navMeshHandler = GetComponent<NavMeshHandler>();
        }

        protected virtual void Start()
        {
            Initialize();
            SetBehaviour();
        }

        protected virtual void Initialize()
        {
            if (!behaviourTree)
            {
                behaviourTree = gameObject.AddComponent<BehaviourTree>();
            }

            if (!navMeshHandler)
            {
                navMeshHandler = gameObject.AddComponent<NavMeshHandler>();
            }

            if (!targetHandler)
            {
                targetHandler = gameObject.AddComponent<TargetHandler>();
            }
        }

        public virtual void SetBehaviour()
        {

        }
        #endregion


        public virtual void RestartBehaviour()
        {
            SetBehaviour();
            if (behaviourTree)
            {
                behaviourTree.enabled = true;
                behaviourTree.Initialize();
            }
        }

        public virtual void StopBehaviour()
        {
            if (behaviourTree)
            {
                behaviourTree.Stop();
                behaviourTree.enabled = false;
                behaviourTree.StopAllCoroutines();
            }
        }
    }
}