using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTWasCalled : BTNode
    {
        private TargetController _target;
        private float _distanceCall;

        public BTWasCalled(TargetController target, float distanceCall)
        {
            _target = target;
            _distanceCall = distanceCall;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            GameObject[] calls = GameObject.FindGameObjectsWithTag("CallCounter");
            int i = 0;

            foreach (GameObject call in calls)
            {
                if (bt.gameObject == call) continue;

                if (Vector3.Distance(bt.transform.position, call.transform.position) < _distanceCall)
                {
                    _target.SetTarget(call.transform);
                    i++;
                    if (i == 8)
                    {
                        _target.SetTarget(null);
                        i = 0;
                    }
                    CurrentStatus = Status.SUCCESS;
                    yield break;
                }
            }
            yield break;
        }
    }
}