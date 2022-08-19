using System.Collections;
using UnityEngine;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTCollect : BTNode
    {
        public string target;
        public float distance = 1;

        public BTCollect(string _target, float _distance)
        {
            target = _target;
            distance = _distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            Transform npc = bt.transform;
            GameObject[] items = GameObject.FindGameObjectsWithTag(target);

            foreach (GameObject item in items)
            {
                if (Vector3.Distance(npc.position, item.transform.position) < distance)
                {
                    GameObject.Destroy(item);
                    status = Status.SUCCESS;
                }
            }
            yield break;
        }
    }
}