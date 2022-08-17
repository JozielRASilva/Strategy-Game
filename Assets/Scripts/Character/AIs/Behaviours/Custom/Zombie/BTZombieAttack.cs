using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieDiorama.Character.AIs.Behaviours.Custom.Zombie
{
    public class BTZombieAttack : BTNode
    {
        GameObject hitboxes;
        float coolDown;
        private EventCaller eventCaller;

        public BTZombieAttack(GameObject _hitboxes, float _coolDown, EventCaller _eventCaller)
        {
            hitboxes = _hitboxes;
            coolDown = _coolDown;
            eventCaller = _eventCaller;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            yield return new WaitForSeconds(coolDown);

            eventCaller.FirstCall();
            hitboxes.SetActive(true);

            yield return new WaitForSeconds(coolDown);

            hitboxes.SetActive(false);
            status = Status.SUCCESS;

            yield break;
        }
    }
}