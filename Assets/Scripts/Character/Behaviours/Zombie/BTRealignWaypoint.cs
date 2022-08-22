using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTRealignWaypoint : BTNode
    {
        public int _currentWaypoint;

        private TargetController _target;
        private Transform[] _waypoints;

        public BTRealignWaypoint(Transform[] waypoints, TargetController target)
        {
            _waypoints = waypoints;
            _target = target;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            _currentWaypoint++;

            if (_currentWaypoint >= _waypoints.Length)
            {
                _currentWaypoint = 0;
            }

            _target.SetTarget(_waypoints[_currentWaypoint]);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}