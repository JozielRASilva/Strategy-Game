using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.AIs.Controllers;

namespace ZombieDiorama.Character.AIs.Behaviours.Custom.Zombie
{
    public class BTRealignWaypoint : BTNode
    {
        private Transform[] waypoints;
        public int currentWaypoint;
        TargetController target;

        public BTRealignWaypoint(Transform[] _waypoints, TargetController _target)
        {
            waypoints = _waypoints;
            target = _target;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }

            target.SetTarget(waypoints[currentWaypoint]);
            status = Status.SUCCESS;

            yield break;
        }
    }
}