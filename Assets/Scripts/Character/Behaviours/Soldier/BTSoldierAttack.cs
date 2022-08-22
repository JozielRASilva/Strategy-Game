using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTSoldierAttack : BTNode
    {
        private TargetController _targetZombie;
        private ShootHandler _shootHandler;
        private float _coolDown;
        private float _damping;
        private string _targetTag;
        private EventCaller _eventCaller;

        public BTSoldierAttack(TargetController targetZombie, float coolDown, ShootHandler shootHandler, float damping, string targetTag)
        {
            _targetZombie = targetZombie;
            _coolDown = coolDown;
            _shootHandler = shootHandler;
            _targetTag = targetTag;
            _damping = damping;
        }

        public BTSoldierAttack(TargetController targetZombie, float coolDown, ShootHandler shootHandler, float damping, string targetTag, EventCaller eventCaller)
        {
            _targetZombie = targetZombie;
            _coolDown = coolDown;
            _shootHandler = shootHandler;
            _targetTag = targetTag;
            _damping = damping;
            _eventCaller = eventCaller;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            CurrentStatus = Status.RUNNING;

            GameObject selectedEnemy = GetTarget(bt.transform);

            if (selectedEnemy)
            {
                float timeStamp = Time.time + _coolDown;
                while (timeStamp > Time.time)
                {
                    var lookPos = selectedEnemy.transform.position - bt.transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, rotation, Time.deltaTime * _damping);

                    yield return null;
                }

                _shootHandler.Execute();

                if (_eventCaller)
                    _eventCaller.FirstCall();

                CurrentStatus = Status.SUCCESS;
            }
            else
            {
                CurrentStatus = Status.FAILURE;
            }
        }

        public GameObject GetTarget(Transform current)
        {
            GameObject selected = null;
            GameObject[] targets = GameObject.FindGameObjectsWithTag(_targetTag);
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