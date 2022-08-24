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
        protected TargetHandler TargetHandler;
        protected NavMeshHandler NavMeshHandler;
        protected BehaviourTree behaviourTree;

        #region  SETUP
        protected virtual void Awake()
        {
            TargetHandler = GetComponent<TargetHandler>();
            NavMeshHandler = GetComponent<NavMeshHandler>();
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

            if (!NavMeshHandler)
            {
                NavMeshHandler = gameObject.AddComponent<NavMeshHandler>();
            }

            if (!TargetHandler)
            {
                TargetHandler = gameObject.AddComponent<TargetHandler>();
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