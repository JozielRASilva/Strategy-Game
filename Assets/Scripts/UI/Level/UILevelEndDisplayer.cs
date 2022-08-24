using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZombieDiorama.Level;

public class UILevelEndDisplayer : MonoBehaviour
{
    [Header("Win Setup")]
    public UnityEvent EventOnWin;

    [Header("Lose Setup")]
    public UnityEvent EventOnLose;

    private void Awake()
    {
        LevelManager.OnWin += WinProcedure;
        LevelManager.OnLose += LoseProcedure;
    }

    private void OnDestroy()
    {
        LevelManager.OnWin -= WinProcedure;
        LevelManager.OnLose -= LoseProcedure;
    }

    private void WinProcedure()
    {
        EventOnWin?.Invoke();
    }

    private void LoseProcedure()
    {
        EventOnLose?.Invoke();
    }
}
