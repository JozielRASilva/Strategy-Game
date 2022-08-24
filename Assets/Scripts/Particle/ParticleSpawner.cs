using MonsterLove.Pooller;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZombieDiorama.Particles
{
    public class ParticleSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("particle")] public GameObject Particle;
        [FormerlySerializedAs("poolSize")] public int PoolSize = 5;

        private void Awake()
        {
            if (Particle)
                PoolManager.WarmPool(Particle, PoolSize);
        }

        public void Spawn()
        {
            if (Particle)
                PoolManager.SpawnObject(Particle, transform.position, transform.rotation);
        }
    }
}