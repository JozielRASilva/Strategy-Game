using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZombieDiorama.Character.AIs.Controllers;

namespace ZombieDiorama.Character.AIs.Behaviours.Custom.Soldier.Heal
{
    public class BTHitbox : BTNode
    {
        private GameObject hitboxes;
        private float coolDown;
        private float rest;
        private TargetController targetController;
        private float damping = 1;
        private EventCaller OnHit;

        public BTHitbox(GameObject _hitboxes, float _coolDown, float _rest, TargetController _targetController, EventCaller _OnHit)
        {
            hitboxes = _hitboxes;
            coolDown = _coolDown;
            targetController = _targetController;
            rest = _rest;
            OnHit = _OnHit;
        }

        public BTHitbox(GameObject _hitboxes, float _coolDown, float _rest, TargetController _targetController, float _damping, EventCaller _OnHit)
        {
            hitboxes = _hitboxes;
            coolDown = _coolDown;
            targetController = _targetController;
            rest = _rest;
            damping = _damping;
            OnHit = _OnHit;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            float timeStamp = Time.time + coolDown;

            while (timeStamp > Time.time)
            {
                var lookPos = targetController.GetTarget().position - bt.transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, rotation, Time.deltaTime * damping);
                yield return null;
            }

            if (OnHit)
                OnHit.FirstCall();
            hitboxes.SetActive(true);

            yield return new WaitForSeconds(rest);

            hitboxes.SetActive(false);
            status = Status.SUCCESS;

            yield break;
        }
    }
}
