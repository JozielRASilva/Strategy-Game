using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTSeeSoldier : BTNode
    {
        private TargetHandler targetSoldier;
        private float distanceView;
        private string soldierTag;

        public BTSeeSoldier(TargetHandler _targetSoldier, float _distanceView, string _soldierTag)
        {
            targetSoldier = _targetSoldier;
            distanceView = _distanceView;
            soldierTag = _soldierTag;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.FAILURE;

            List<GameObject> soldiers = TagObjectsCacher.GetObjects(soldierTag);

            foreach (GameObject soldier in soldiers)
            {
                if (bt.gameObject == soldier) continue;
                if (!soldier.activeSelf) continue;
                if (Vector3.Distance(bt.transform.position, soldier.transform.position) < distanceView)
                {
                    targetSoldier.SetTarget(soldier.transform);
                    CurrentStatus = Status.SUCCESS;
                    break;
                }
            }
            yield break;
        }
    }
}