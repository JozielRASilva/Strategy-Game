using MonsterLove.Pooller;
using UnityEngine;

namespace ZombieDiorama.Particles
{
    public class ParticleSpawner : MonoBehaviour
    {
        public GameObject particle;
        public int poolSize = 5;

        private void Awake()
        {
            if (particle)
                PoolManager.WarmPool(particle, poolSize);
        }

        public void Spawn()
        {
            if (particle)
                PoolManager.SpawnObject(particle, transform.position, transform.rotation);
        }
    }
}