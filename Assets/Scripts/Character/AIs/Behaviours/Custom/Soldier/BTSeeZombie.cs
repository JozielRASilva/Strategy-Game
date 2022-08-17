using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.AIs.Controllers;

namespace ZombieDiorama.Character.AIs.Behaviours.Custom.Soldier
{
    public class BTSeeZombie : BTNode
    {
        private TargetController targetZombie;
        private float distanceView;

        public BTSeeZombie(TargetController _targetZombie, float _distanceView)
        {
            targetZombie = _targetZombie;
            distanceView = _distanceView;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.FAILURE;

            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            foreach (GameObject zombie in zombies)
            {
                if (bt.gameObject == zombie) continue;
                if (Vector3.Distance(bt.transform.position, zombie.transform.position) < distanceView)
                {
                    targetZombie.SetTarget(zombie.transform);
                    status = Status.SUCCESS;
                    break;
                }
            }
            yield break;
        }
    }
}