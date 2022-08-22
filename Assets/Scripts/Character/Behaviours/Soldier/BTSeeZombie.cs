using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTSeeZombie : BTNode
    {
        private TargetController _targetZombie;
        private float _distanceView;

        public BTSeeZombie(TargetController targetZombie, float distanceView)
        {
            _targetZombie = targetZombie;
            _distanceView = distanceView;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            foreach (GameObject zombie in zombies)
            {
                if (bt.gameObject == zombie) continue;
                if (Vector3.Distance(bt.transform.position, zombie.transform.position) < _distanceView)
                {
                    _targetZombie.SetTarget(zombie.transform);
                    CurrentStatus = Status.SUCCESS;
                    break;
                }
            }
            yield break;
        }
    }
}