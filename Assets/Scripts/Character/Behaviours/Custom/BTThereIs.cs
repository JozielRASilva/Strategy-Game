using UnityEngine;
using System.Collections;
using ZombieDiorama.Character.Controllers;
using UnityEngine.Serialization;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTThereIs : BTNode
    {
        public string TargetTag;
        private TargetController _targetController;

        public BTThereIs(TargetController _targetController, string _target)
        {
            TargetTag = _target;
            this._targetController = _targetController;
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
                    _targetController.SetTarget(target);
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
            GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTag);
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