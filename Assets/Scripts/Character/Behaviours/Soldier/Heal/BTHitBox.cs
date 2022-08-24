using System.Collections;
using UnityEngine;
using ZombieDiorama.Character.Handler;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.Behaviours.Soldier
{
    public class BTHitbox : BTNode
    {
        private GameObject hitboxes;
        private float coolDown;
        private float rest;
        private TargetHandler targetHandler;
        private float damping = 1;
        private EventCaller onHit;

        public BTHitbox(GameObject _hitboxes, float _coolDown, float _rest, TargetHandler _targetHandler, EventCaller _onHit)
        {
            hitboxes = _hitboxes;
            coolDown = _coolDown;
            targetHandler = _targetHandler;
            rest = _rest;
            onHit = _onHit;
        }

        public BTHitbox(GameObject _hitboxes, float _coolDown, float _rest, TargetHandler _targetHandler, float _damping, EventCaller _onHit)
        {
            hitboxes = _hitboxes;
            coolDown = _coolDown;
            targetHandler = _targetHandler;
            rest = _rest;
            damping = _damping;
            onHit = _onHit;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            float timeStamp = Time.time + coolDown;
            if (onHit)
                onHit.SecondCall();
            while (timeStamp > Time.time)
            {
                var lookPos = targetHandler.GetTarget().position - bt.transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, rotation, Time.deltaTime * damping);
                yield return null;
            }

            if (onHit)
                onHit.FirstCall();
            hitboxes.SetActive(true);

            yield return new WaitForSeconds(rest);

            hitboxes.SetActive(false);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}
