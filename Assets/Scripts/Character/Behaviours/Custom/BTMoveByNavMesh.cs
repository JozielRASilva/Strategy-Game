using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTMoveByNavMesh : BTNode
    {

        private TargetHandler targetHandler;
        private float speed = 1;
        private float distance = 1;
        private NavMeshHandler navMeshHandler;

        public BTMoveByNavMesh(NavMeshHandler _navMeshHandler, TargetHandler _targetHandler, float _speed, float _distance)
        {
            targetHandler = _targetHandler;
            speed = _speed;
            distance = _distance;
            navMeshHandler = _navMeshHandler;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;

            Transform npc = bt.transform;
            Transform target = null;

            while (true)
            {
                target = targetHandler.GetTarget();

                if (!target || !navMeshHandler)
                {
                    CurrentStatus = Status.FAILURE;
                    break;
                }

                if (Vector3.Distance(npc.position, target.position) < distance) break;

                navMeshHandler.SetTarget(target, speed, distance);

                yield return null;
            }

            navMeshHandler.StopMove();

            if (CurrentStatus.Equals(Status.RUNNING))
                CurrentStatus = Status.SUCCESS;
        }
    }
}