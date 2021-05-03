using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BehaviourTree : MonoBehaviour
{

    private string nodes;

    [Title("Nodes status")]
    [OnInspectorGUI]
    private void Nodes()
    {
        GUILayout.Label($"{nodes}");
    }

    private BTNode root;

    void Start()
    {
        StartCoroutine(Execute());
    }

    public void Build(BTNode _root)
    {
        root = _root;
    }

    IEnumerator Execute()
    {
        while (true)
        {
            if (root != null) yield return StartCoroutine(root.Run(this));
            else yield return null;
        }
    }

    private void LateUpdate() {
        nodes = GetNodes();
    }

    private string GetNodes()
    {
        return GetWriteNode(root);
    }

    private string GetWriteNode(BTNode node)
    {
        string value = $"{node.ToString()} : {node.status.ToString()}";

        foreach (var _node in node.children)
        {
            value += $"\n {GetWriteNode(_node)}";
        }

        return value;
    }

}
