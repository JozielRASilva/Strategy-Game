using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReagroupController : MonoBehaviour
{

    public static ReagroupController Instance;

    public Vector3 ReagroupPoint;
    public int currentReagroupId = -1;

    public bool TimeLimitToReagroup = false;
    public float reagroupTime = 10;
    public float TimeStamp;

    public Transform pointReference;

    private void Awake()
    {
        Instance = GetComponent<ReagroupController>();

        currentReagroupId = -1;
    }

    public void SetPoint(Vector3 position)
    {
        ReagroupPoint = position;

        currentReagroupId++;

        TimeStamp = Time.time + reagroupTime;
    }

    private void Update()
    {
        if (CanReagroup())
        {
            pointReference.gameObject.SetActive(true);

            pointReference.position = ReagroupPoint;
        }
        else
        {
            pointReference.gameObject.SetActive(false);
        }
    }

    public Transform GetReagroupPoint()
    {
        pointReference.position = ReagroupPoint;
        return pointReference;
    }

    public int GetReagroupId()
    {
        return currentReagroupId;
    }

    public bool CanReagroup(int id = -1)
    {
        if (currentReagroupId < 0) return false;

        if (TimeLimitToReagroup)
        {
            if (Time.time > TimeStamp) return false;
        }

        if (id != currentReagroupId) return true;

        return false;
    }


}
