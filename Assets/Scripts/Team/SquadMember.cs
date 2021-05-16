using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SquadMember : MonoBehaviour
{

    public bool ExtraMember;

    [Button("Get Function")]
    public void CheckSquadFunction()
    {
        if (!TeamManager.Instance) return;

        Squad.SquadFunction function = GetSquadFunction();

        Debug.Log($"{name}: {function.ToString()}");

    }

    public Squad.SquadFunction GetSquadFunction()
    {
        return TeamManager.Instance.GetSquadFunction(this);
    }

}
