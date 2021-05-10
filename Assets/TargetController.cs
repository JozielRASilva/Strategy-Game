using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public Transform _currentTarget;

    public Transform GetTarget()
    {
        return _currentTarget;
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }

}
