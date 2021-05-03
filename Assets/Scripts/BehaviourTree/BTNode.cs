using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode
{

    public enum Status { RUNNING, SUCCESS, FAILURE }

    public Status status;

    public List<BTNode> children = new List<BTNode>();

    public abstract IEnumerator Run(BehaviourTree bt);

    public void Print(string text = "")
    {
        string color = "lightblue";
        if (status == Status.SUCCESS) color = "green";
        if (status == Status.SUCCESS) color = "orange";

        Debug.Log($"<color={color}> {this.ToString()} : {status.ToString()} - {text} </color>");

    }

    public void SetNode(BTNode node){
        children.Add(node);
    }



}
