using MonsterLove.Pooller;
using UnityEngine;

namespace ZombieDiorama.Particles
{
    public class ParticleSpawner : MonoBehaviour
    {
        public GameObject Particle;
        public int PoolSize = 5;

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