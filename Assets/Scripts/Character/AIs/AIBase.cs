using UnityEngine;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Character.Behaviours;

namespace ZombieDiorama.Character.AIs
{
    public class AIBase : MonoBehaviour
    {
        protected TargetController _targetController;
        protected NavMeshController _navMeshController;
        protected BehaviourTree _behaviourTree;

        #region  SETUP
        protected virtual void Awake()
        {
            _targetController = GetComponent<TargetController>();
            _navMeshController = GetComponent<NavMeshController>();
        }

        protected virtual void Start()
        {
            Initialize();
            SetBehaviour();
        }

        protected virtual void Initialize()
        {
            if (!_behaviourTree)
            {
                _behaviourTree = gameObject.AddComponent<BehaviourTree>();
            }

            if (!_navMeshController)
            {
                _navMeshController = gameObject.AddComponent<NavMeshController>();
            }

            if (!_targetController)
            {
                _targetController = gameObject.AddComponent<TargetController>();
            }
        }

        public virtual void SetBehaviour()
        {

        }
        #endregion


        public virtual void RestartBehaviour()
        {
            SetBehaviour();
            if (_behaviourTree)
            {
                _behaviourTree.enabled = true;
                _behaviourTree.Initialize();
            }
        }

        public virtual void StopBehaviour()
        {
            if (_behaviourTree)
            {
                _behaviourTree.Stop();
                _behaviourTree.enabled = false;
                _behaviourTree.StopAllCoroutines();
            }
        }
    }
}