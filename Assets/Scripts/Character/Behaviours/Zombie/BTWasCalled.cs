using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTWasCalled : BTNode
    {
        private TargetHandler target;
        private float distanceCall;
        private string callerTag;

        public BTWasCalled(TargetHandler _target, float _distanceCall, string _callerTag)
        {
            target = _target;
            distanceCall = _distanceCall;
            callerTag = _callerTag;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            List<GameObject> calls = TagObjectsCacher.GetObjects(callerTag);
            int i = 0;
            
            foreach (GameObject call in calls)
            {
                if (bt.gameObject == call) continue;

                if (Vector3.Distance(bt.transform.position, call.transform.position) < distanceCall)
                {
                    target.SetTarget(call.transform);
                    i++;
                    if (i == 8)
                    {
                        target.SetTarget(null);
                        i = 0;
                    }
                    status = Status.SUCCESS;
                    yield break;
                }
            }
            yield break;
        }
    }
}