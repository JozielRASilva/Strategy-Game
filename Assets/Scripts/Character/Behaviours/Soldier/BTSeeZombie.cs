using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTSeeZombie : BTNode
    {
        private TargetHandler targetZombie;
        private float distanceView;
        private string zombieTag;

        public BTSeeZombie(TargetHandler _targetZombie, float _distanceView, string _zombieTag)
        {
            targetZombie = _targetZombie;
            distanceView = _distanceView;
            zombieTag = _zombieTag;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            List<GameObject> zombies = TagObjectsCacher.GetObjects(zombieTag);
            foreach (GameObject zombie in zombies)
            {
                if (bt.gameObject == zombie || !zombie.activeInHierarchy) continue;
                if (Vector3.Distance(bt.transform.position, zombie.transform.position) < distanceView)
                {
                    targetZombie.SetTarget(zombie.transform);
                    CurrentStatus = Status.SUCCESS;
                    break;
                }
            }
            yield break;
        }
    }
}