using UnityEngine;
using System.Collections;
using ZombieDiorama.Character.Handler;
using System.Collections.Generic;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTThereIs : BTNode
    {
        public string TargetTag;
        private TargetHandler targetHandler;

        public BTThereIs(TargetHandler _targetHandler, string _target)
        {
            TargetTag = _target;
            targetHandler = _targetHandler;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;
            if (TargetTag == "")
            {
                CurrentStatus = Status.FAILURE;
                yield break;
            }

            if (GameObject.FindGameObjectWithTag(TargetTag))
            {
                Transform target = GetTarget(bt.transform);
                if (target)
                {
                    targetHandler.SetTarget(target);
                }
                CurrentStatus = Status.SUCCESS;
            }
            else
            {
                CurrentStatus = Status.FAILURE;
            }
            yield break;
        }


        public Transform GetTarget(Transform current)
        {
            GameObject selected = null;
            List<GameObject> targets = TagObjectsCacher.GetObjects(TargetTag);
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