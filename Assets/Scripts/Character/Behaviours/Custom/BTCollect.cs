using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTCollect : BTNode
    {
        private string target;
        private float distance = 1;

        public BTCollect(string _target, float _distance)
        {
            target = _target;
            distance = _distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            Transform npc = bt.transform;
            List<GameObject> items = TagObjectsCacher.GetObjects(target);

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