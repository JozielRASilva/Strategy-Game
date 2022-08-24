using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Utilities.Events;
using ZombieDiorama.Utilities.TagsCacher;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTSoldierAttack : BTNode
    {
        private TargetController targetZombie;
        private ShootHandler shootHandler;
        private float coolDown;
        private float damping;
        private string targetTag;
        private EventCaller eventCaller;

        public BTSoldierAttack(TargetController _targetZombie, float _coolDown, ShootHandler _shootHandler, float _damping, string _targetTag)
        {
            targetZombie = _targetZombie;
            coolDown = _coolDown;
            shootHandler = _shootHandler;
            targetTag = _targetTag;
            damping = _damping;
        }

        public BTSoldierAttack(TargetController _targetZombie, float _coolDown, ShootHandler _shootHandler, float _damping, string _targetTag, EventCaller _eventCaller)
        {
            targetZombie = _targetZombie;
            coolDown = _coolDown;
            shootHandler = _shootHandler;
            targetTag = _targetTag;
            damping = _damping;
            eventCaller = _eventCaller;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            status = Status.RUNNING;

            GameObject selectedEnemy = GetTarget(bt.transform);

            if (selectedEnemy)
            {
                float timeStamp = Time.time + coolDown;
                while (timeStamp > Time.time)
                {
                    var lookPos = selectedEnemy.transform.position - bt.transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, rotation, Time.deltaTime * damping);

                    if (!selectedEnemy.activeInHierarchy)
                        yield break;

                    yield return null;
                }

                shootHandler.Execute();

                if (eventCaller)
                    eventCaller.FirstCall();

                status = Status.SUCCESS;
            }
            else
            {
                status = Status.FAILURE;
            }
        }

        public GameObject GetTarget(Transform current)
        {
            GameObject selected = null;
            List<GameObject> targets = TagObjectsCacher.GetObjects(targetTag);
            float lastDistance = 0;

            foreach (var _target in targets)
            {
                if (_target == current.gameObject) continue;
                float distance = Vector3.Distance(current.position, _target.transform.position);
                if (!selected)
                {
                    selected = _target;
                    lastDistance = distance;
                }
                else
                {
                    if (distance < lastDistance)
                    {
                        selected = _target;
                        lastDistance = distance;
                    }
                }
            }

            if (selected) return selected;
            else return null;
        }
    }
}