using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieDiorama.Character.Behaviours
{
    public abstract class BTNode
    {
        public enum Status { RUNNING, SUCCESS, FAILURE }
        public Status CurrentStatus;
        public List<BTNode> Children = new List<BTNode>();
        public abstract IEnumerator Run(BehaviourTree bt);

        public void Print(string text = "")
        {
            string color = "lightblue";
            if (CurrentStatus == Status.SUCCESS) color = "green";
            if (CurrentStatus == Status.SUCCESS) color = "orange";

            Debug.Log($"<color={color}> {this.ToString()} : {CurrentStatus.ToString()} - {text} </color>");
        }

        public void SetNode(BTNode node)
        {
            Children.Add(node);
        }
    }
}