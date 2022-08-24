using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.Pooller;

public class ShootHandler : MonoBehaviour
{
    [Header("Setup")]
    public float Force = 4;
    public GameObject Muzzle;

    [Header("Pool")]
    public Projectile Prefab;
    public int PoolSize = 20;

    private void Start()
    {
        PoolManager.WarmPool(Prefab.gameObject, PoolSize);
    }

    public void Execute()
    {
        Vector3 position = Muzzle.transform.position;
        Projectile shoot = PoolManager.SpawnObject(Prefab.gameObject, position, Quaternion.identity).GetComponent<Projectile>();
        shoot.Initialize(Muzzle.transform.forward * Force);
    }

}
