using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SquadMember : MonoBehaviour
{

    public bool ExtraMember;

    public Health health;

    private void Awake()
    {
        if (!health)
        {
            health = GetComponent<Health>();
        }
    }

    [Button("Get Function")]
    public void CheckSquadFunction()
    {
        if (!TeamManager.Instance) return;

        Squad.SquadFunction function = GetSquadFunction();

    }

    [Button("Remove Function")]
    public void RemoveFromSquad()
    {
        if (!TeamManager.Instance) return;

        TeamManager.Instance.RemoveFromSquad(this);

    }

    public Squad.SquadFunction GetSquadFunction()
    {
        return TeamManager.Instance.GetSquadFunction(this);
    }

}
