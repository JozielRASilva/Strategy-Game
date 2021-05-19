using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public interface IKillableObject
{
    IEnumerator DestroyCO();
    void Kill();
}