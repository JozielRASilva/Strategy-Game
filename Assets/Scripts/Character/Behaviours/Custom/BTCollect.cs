using System.Collections;
using UnityEngine;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTCollect : BTNode
    {
        public string Target;
        public float Distance = 1;

        public BTCollect(string _target, float _distance)
        {
            Target = _target;
            Distance = _distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            Transform npc = bt.transform;
            GameObject[] items = GameObject.FindGameObjectsWithTag(Target);

            foreach (GameObject item in items)
            {
                if (Vector3.Distance(npc.position, item.transform.position) < Distance)
                {
                    GameObject.Destroy(item);
                    CurrentStatus = Status.SUCCESS;
                }
            }
            yield break;
        }
    }
}