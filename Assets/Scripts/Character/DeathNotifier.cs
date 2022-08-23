using UnityEngine;
using ZombieDiorama.Utilities.Patterns;

namespace ZombieDiorama.Character
{
    [RequireComponent(typeof(Health))]
    public class DeathNotifier : MonoBehaviour
    {
        public ObserverEvent Tag;

        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void Start()
        {
            health.ActionOnKill += NotifyDeath;
        }

        private void NotifyDeath()
        {
            Observer.Notify(Tag);
        }
    }
}