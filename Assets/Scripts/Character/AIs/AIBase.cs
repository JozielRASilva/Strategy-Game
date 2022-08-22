using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Behaviours;

namespace ZombieDiorama.Character.AIs
{
    public class AIBase : MonoBehaviour
    {
        protected TargetController TargetController;
        protected NavMeshController NavMeshController;
        protected BehaviourTree behaviourTree;

        #region  SETUP
        protected virtual void Awake()
        {
            TargetController = GetComponent<TargetController>();
            NavMeshController = GetComponent<NavMeshController>();
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

            if (!NavMeshController)
            {
                NavMeshController = gameObject.AddComponent<NavMeshController>();
            }

            if (!TargetController)
            {
                TargetController = gameObject.AddComponent<TargetController>();
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