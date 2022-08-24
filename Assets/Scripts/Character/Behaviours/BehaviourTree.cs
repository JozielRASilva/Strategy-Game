using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ZombieDiorama.Character.Behaviours
{
    public class BehaviourTree : MonoBehaviour
    {
        private string nodes;

        [Title("Nodes status")]
        [OnInspectorGUI]
        private void Nodes()
        {
            GUILayout.Label($"{nodes}");
        }

        public bool Debug;
        private BTNode root;
        public Coroutine execution;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            execution = StartCoroutine(Execute());
        }

        public void Stop()
        {
            if (execution != null)
                StopCoroutine(execution);
        }

        public void Build(BTNode _root)
        {
            root = _root;
        }

        private IEnumerator Execute()
        {
            while (true)
            {
                if (root != null) yield return StartCoroutine(root.Run(this));
                else yield return null;
            }
        }

        private void LateUpdate()
        {
#if UNITY_EDITOR
            if (Debug)
                nodes = GetNodes();
#endif
        }

        private string GetNodes()
        {
            return GetWriteNode(root);
        }

        private string GetWriteNode(BTNode node)
        {
            string value = $"{node.ToString()} : {node.CurrentStatus.ToString()}";
            foreach (var _node in node.Children)
            {
                value += $"\n {GetWriteNode(_node)}";
            }
            return value;
        }

    }
}