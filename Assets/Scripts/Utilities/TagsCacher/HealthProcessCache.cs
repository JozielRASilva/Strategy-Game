using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Character;

namespace ZombieDiorama.Utilities.TagsCacher
{
    [RequireComponent(typeof(Health), typeof(RegisterTagCache))]
    public class HealthProcessCache : MonoBehaviour
    {
        private Health health;
        private RegisterTagCache registerTagCache;

        private void Awake()
        {
            health = GetComponent<Health>();
            registerTagCache = GetComponent<RegisterTagCache>();

            health.ActionOnKill += RemoveCache;
            health.OnRespawn += AddCache;
        }

        private void RemoveCache()
        {
            registerTagCache?.UnRegisterCache();
        }

        private void AddCache()
        {
            registerTagCache?.RegisterCache();
        }
    }
}