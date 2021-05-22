using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    public Action OnWin;
    public Action OnLose;

    public List<string> enemiesTag = new List<string>();
    public List<string> alliesTag = new List<string>();

    public List<Health> Zombies = new List<Health>();
    public List<Health> Soldiers = new List<Health>();

    [Button("Check Enemies")]
    private void BEnemiesAlive() => Debug.Log($"Enemies Alive: {EnemiesAlive()}");

    [Button("Check Allies")]
    private void BSoldiersAlive() => Debug.Log($"Allies Alive: {SoldiersAlive()}");

    [SerializeField]
    private List<Health> AllCharacters = new List<Health>();

    private bool levelFinished;

    private void Awake()
    {
        Instance = this;

        UpdateCharacters();
    }

    private void Update()
    {
        if (levelFinished) return;

        if (!EnemiesAlive())
        {
            OnWin?.Invoke();

            levelFinished = true;
        }
        else if (!SoldiersAlive())
        {
            OnLose?.Invoke();

            levelFinished = true;
        }


    }

    private bool EnemiesAlive()
    {
        var enemiesAlive = Zombies.FindAll(zombie => zombie.IsAlive());

        if (enemiesAlive.Count > 0)
            return true;
        else
            return false;
    }

    private bool SoldiersAlive()
    {
        var soldiersAlive = Soldiers.FindAll(soldier => soldier.IsAlive());

        if (soldiersAlive.Count > 0)
            return true;
        else
            return false;
    }

    private void UpdateCharacters()
    {
        AllCharacters = FindObjectsOfType<Health>(true).ToList();

        Zombies = AllCharacters.FindAll(zombie => enemiesTag.Contains(zombie.tag));

        Soldiers = AllCharacters.FindAll(soldiers => alliesTag.Contains(soldiers.tag));

    }


}
