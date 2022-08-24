using UnityEngine;
using System.Collections;
using ZombieDiorama.Character.Handler;
using System.Collections.Generic;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTThereIs : BTNode
    {
        public string targetTag;
        private TargetHandler targetController;

        public BTThereIs(TargetHandler _targetController, string _target)
        {
            targetTag = _target;
            targetController = _targetController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.RUNNING;
            if (targetTag == "")
            {
                status = Status.FAILURE;
                yield break;
            }

            if (GameObject.FindGameObjectWithTag(targetTag))
            {
                Transform target = GetTarget(bt.transform);
                if (target)
                {
                    targetController.SetTarget(target);
                }
                status = Status.SUCCESS;
            }
            else
            {
                status = Status.FAILURE;
            }
            yield break;
        }


        public Transform GetTarget(Transform current)
        {
            GameObject selected = null;
            List<GameObject> targets = TagObjectsCacher.GetObjects(targetTag);
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