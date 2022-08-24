using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTRealignWaypoint : BTNode
    {
        private Transform[] waypoints;
        private int currentWaypoint;
        private TargetHandler target;

        public BTRealignWaypoint(Transform[] _waypoints, TargetHandler _target)
        {
            waypoints = _waypoints;
            target = _target;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }

            target.SetTarget(waypoints[currentWaypoint]);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}