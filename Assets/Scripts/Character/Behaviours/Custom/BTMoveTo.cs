using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTMoveTo : BTNode
    {
        public string target;
        public float speed = 1;
        public float distance = 1;

        public BTMoveTo(string _target, float _speed, float _distance)
        {
            target = _target;
            speed = _speed;
            distance = _distance;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.RUNNING;
            Transform npc = bt.transform;
            Transform target = GetTarget(npc);

            while (true)
            {
                if (!target)
                {
                    status = Status.FAILURE;
                    break;
                }

                if (Vector3.Distance(npc.position, target.position) < distance) break;

                npc.LookAt(target);
                npc.position += npc.forward * Time.deltaTime * speed;
                yield return null;
            }
            if (status.Equals(Status.RUNNING))
                status = Status.SUCCESS;
        }

        public Transform GetTarget(Transform current)
        {
            GameObject selected = null;
            List<GameObject> targets = TagObjectsCacher.GetObjects(target);
            float lastDistance = 0;

            foreach (var _target in targets)
            {
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
}