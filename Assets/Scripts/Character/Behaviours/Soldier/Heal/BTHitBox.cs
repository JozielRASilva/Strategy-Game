using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Controllers;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTHitbox : BTNode
    {
        private GameObject _hitboxes;
        private float _coolDown;
        private float _rest;
        private TargetController _targetController;
        private float _damping = 1;
        private EventCaller _onHit;

        public BTHitbox(GameObject hitboxes, float coolDown, float rest, TargetController targetController, EventCaller onHit)
        {
            _hitboxes = hitboxes;
            _coolDown = coolDown;
            _targetController = targetController;
            _rest = rest;
            _onHit = onHit;
        }

        public BTHitbox(GameObject hitboxes, float coolDown, float rest, TargetController targetController, float damping, EventCaller onHit)
        {
            _hitboxes = hitboxes;
            _coolDown = coolDown;
            _targetController = targetController;
            _rest = rest;
            _damping = damping;
            _onHit = onHit;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            float timeStamp = Time.time + _coolDown;

            while (timeStamp > Time.time)
            {
                var lookPos = _targetController.GetTarget().position - bt.transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, rotation, Time.deltaTime * _damping);
                yield return null;
            }

            if (_onHit)
                _onHit.FirstCall();
            _hitboxes.SetActive(true);

            yield return new WaitForSeconds(_rest);

            _hitboxes.SetActive(false);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}
