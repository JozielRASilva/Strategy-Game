using System.Collections;
using UnityEngine;
using ZombieDiorama.Utilities.Events;

namespace ZombieDiorama.Character.Behaviours.Zombie
{
    public class BTZombieAttack : BTNode
    {
        private GameObject hitboxes;
        private float coolDown;
        private EventCaller onAttack;

        public BTZombieAttack(GameObject _hitboxes, float _coolDown, EventCaller _onAttack)
        {
            hitboxes = _hitboxes;
            coolDown = _coolDown;
            onAttack = _onAttack;
        }

        public override IEnumerator Run(BehaviourTree bt)
        {
            yield return new WaitForSeconds(coolDown);

            onAttack.FirstCall();
            hitboxes.SetActive(true);

            yield return new WaitForSeconds(coolDown);

            hitboxes.SetActive(false);
            CurrentStatus = Status.SUCCESS;

            yield break;
        }
    }
}