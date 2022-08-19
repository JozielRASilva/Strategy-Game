using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTMoveByNavMesh : BTNode
    {

        private TargetController targetController;
        private float speed = 1;
        private float distance = 1;
        private NavMeshController NavMeshController;

        public BTMoveByNavMesh(NavMeshController _navMeshController, TargetController _targetController, float _speed, float _distance)
        {
            targetController = _targetController;
            speed = _speed;
            distance = _distance;
            NavMeshController = _navMeshController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.RUNNING;

            Transform npc = bt.transform;
            Transform target = null;

            while (true)
            {
                target = targetController.GetTarget();

                if (!target || !NavMeshController)
                {
                    status = Status.FAILURE;
                    break;
                }

                if (Vector3.Distance(npc.position, target.position) < distance) break;

                NavMeshController.SetTarget(target, speed, distance);

                yield return null;
            }

            NavMeshController.StopMove();

            if (status.Equals(Status.RUNNING))
                status = Status.SUCCESS;
        }
    }
}