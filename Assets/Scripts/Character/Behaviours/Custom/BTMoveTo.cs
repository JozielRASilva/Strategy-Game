using System.Collections;
using UnityEngine;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTMoveTo : BTNode
    {
        public string Target;
        public float Speed = 1;
        public float Distance = 1;

        public BTMoveTo(string _target, float _speed, float _distance)
        {
            Target = _target;
            Speed = _speed;
            Distance = _distance;
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

                if (Vector3.Distance(npc.position, target.position) < Distance) break;

                npc.LookAt(target);
                npc.position += npc.forward * Time.deltaTime * Speed;
                yield return null;
            }
            if (status.Equals(Status.RUNNING))
                status = Status.SUCCESS;
        }

        public Transform GetTarget(Transform current)
        {
            GameObject selected = null;
            GameObject[] targets = GameObject.FindGameObjectsWithTag(Target);
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