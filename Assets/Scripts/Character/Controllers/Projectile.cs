using System.Collections;
using MonsterLove.Pooller;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float DisableTimer = 5;

    public Transform Model;

    private Rigidbody rigidbody;
    private WaitForSeconds waitForSeconds;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        waitForSeconds = new WaitForSeconds(DisableTimer);
    }

    public void Initialize(Vector3 force)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(force, ForceMode.Impulse);

        if (Model)
            Model.right = force.normalized;
    }

    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(DisableTimer);
        PoolManager.ReleaseObject(gameObject);
    }
}
