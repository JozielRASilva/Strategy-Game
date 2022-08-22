using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ZombieDiorama.Character.Behaviours
{
    public class BehaviourTree : MonoBehaviour
    {
        private string _nodes;

        [Title("Nodes status")]
        [OnInspectorGUI]
        private void Nodes()
        {
            GUILayout.Label($"{_nodes}");
        }

        private bool _debug;
        private BTNode _root;
        public Coroutine _execution;

        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _execution = StartCoroutine(Execute());
        }

        public void Stop()
        {
            if (_execution != null)
                StopCoroutine(_execution);
        }

        public void Build(BTNode _root)
        {
            this._root = _root;
        }

        IEnumerator Execute()
        {
            while (true)
            {
                if (_root != null) yield return StartCoroutine(_root.Run(this));
                else yield return null;
            }
        }

        private void LateUpdate()
        {
#if UNITY_EDITOR
            if (_debug)
                _nodes = GetNodes();
#endif
        }

        private string GetNodes()
        {
            return GetWriteNode(_root);
        }

        private string GetWriteNode(BTNode node)
        {
            string value = $"{node.ToString()} : {node.CurrentStatus.ToString()}";
            foreach (var _node in node.children)
            {
                value += $"\n {GetWriteNode(_node)}";
            }
            return value;
        }

    }
}