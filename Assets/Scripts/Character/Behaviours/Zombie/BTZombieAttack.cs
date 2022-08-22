using System.Collections;
using UnityEngine;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTZombieAttack : BTNode
    {
        private GameObject _hitboxes;
        private float _coolDown;
        private EventCaller _eventCaller;

        public BTZombieAttack(GameObject hitboxes, float coolDown, EventCaller eventCaller)
        {
            _hitboxes = hitboxes;
            _coolDown = coolDown;
            _eventCaller = eventCaller;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            yield return new WaitForSeconds(_coolDown);

            _eventCaller.FirstCall();
            _hitboxes.SetActive(true);

            yield return new WaitForSeconds(_coolDown);

            _hitboxes.SetActive(false);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}