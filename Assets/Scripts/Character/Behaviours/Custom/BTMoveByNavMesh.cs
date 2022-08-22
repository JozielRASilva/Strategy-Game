using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Custom
{
    public class BTMoveByNavMesh : BTNode
    {
        private TargetController _targetController;
        private float _speed = 1;
        private float _distance = 1;
        private NavMeshController NavMeshController;

        public BTMoveByNavMesh(NavMeshController _navMeshController, TargetController _targetController, float _speed, float _distance)
        {
            this._targetController = _targetController;
            this._speed = _speed;
            this._distance = _distance;
            NavMeshController = _navMeshController;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;

            Transform npc = bt.transform;
            Transform target = null;

            while (true)
            {
                target = _targetController.GetTarget();

                if (!target || !NavMeshController)
                {
                    CurrentStatus = Status.FAILURE;
                    break;
                }

                if (Vector3.Distance(npc.position, target.position) < _distance) break;

                NavMeshController.SetTarget(target, _speed, _distance);

                yield return null;
            }

            NavMeshController.StopMove();

            if (CurrentStatus.Equals(Status.RUNNING))
                CurrentStatus = Status.SUCCESS;
        }
    }
}