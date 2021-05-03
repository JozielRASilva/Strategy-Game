using System.Collections;
using UnityEngine;

public class BTFugirInimigo : BTNode {
    
    public string target = "Inimigo";
    public float speed = 3;
    public float limitTimeToRun = 2;



    public BTFugirInimigo(string _target, float _speed, float _limitTimeToRun )
    {
        target = _target;
        speed = _speed;
        limitTimeToRun = _limitTimeToRun;
      
    }

    public override IEnumerator Run(BehaviourTree bt)
    {

        status = Status.RUNNING;

        Transform npc = bt.transform;

        Transform target = GetTarget(npc);

        float timeStamp = Time.time + limitTimeToRun; 
        while (Time.time < timeStamp)
        {
            if (!target)
            {
                status = Status.FAILURE;
                break;
            }

            Vector3 direction = (npc.transform.position - target.position).normalized;

            npc.LookAt(2 * npc.transform.position - target.position);

            Debug.DrawLine(npc.transform.position, 2 * npc.transform.position - target.position, Color.blue);
            Debug.DrawLine(npc.transform.position, target.position, Color.red);
            
            npc.position += npc.forward * Time.deltaTime * speed;
            
            yield return null;

        }


        if (status.Equals(Status.RUNNING))
            status = Status.SUCCESS;

        

    }

    public Transform GetTarget(Transform current)
    {
        GameObject selected = null;

        GameObject[] targets = GameObject.FindGameObjectsWithTag(target);

        float lastDistance = 0;

        foreach (var _target in targets)
        {
            if (_target == current.gameObject) continue;

            float distance = Vector3.Distance(current.position, _target.transform.position);
            if (!selected)
            {
                selected = _target;
                lastDistance = distance;
            }
            else
            {
                if (distance < lastDistance)
                {
                    selected = _target;
                    lastDistance = distance;
                }

            }

        }

        if (selected) return selected.transform;
        else return null;
    }
    
}