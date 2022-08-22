using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTSeeSoldier : BTNode
    {
        private TargetController _targetSoldier;
        private float _distanceView;

        public BTSeeSoldier(TargetController targetSoldier, float distanceView)
        {
            _targetSoldier = targetSoldier;
            _distanceView = distanceView;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Soldier");

            foreach (GameObject soldier in soldiers)
            {
                if (bt.gameObject == soldier) continue;
                if (!soldier.activeSelf) continue;
                if (Vector3.Distance(bt.transform.position, soldier.transform.position) < _distanceView)
                {
                    _targetSoldier.SetTarget(soldier.transform);
                    CurrentStatus = Status.SUCCESS;
                    break;
                }
            }
            yield break;
        }
    }
}