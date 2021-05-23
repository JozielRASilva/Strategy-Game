using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
