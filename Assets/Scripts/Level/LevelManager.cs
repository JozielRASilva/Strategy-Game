using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    public int targetFrameRate = 60;

    public Action OnWin;
    public Action OnLose;

    public UnityEvent EventOnWin;
    public UnityEvent EventOnLose;

    public List<string> enemiesTag = new List<string>();
    public List<string> alliesTag = new List<string>();

    public List<Health> Zombies = new List<Health>();
    public List<Health> Soldiers = new List<Health>();

    [Title("UI")]

    [Title("Soldier")]
    public Text SoldierTotal;
    public Text SoldierCurrent;


    [Title("Zombie")]
    public Text ZombieTotal;
    public Text ZombieCurrent;

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

        Application.targetFrameRate = targetFrameRate;
    }

    private void Update()
    {
        if (levelFinished) return;

        if (!EnemiesAlive())
        {
            OnWin?.Invoke();
            EventOnWin?.Invoke();
            levelFinished = true;
        }
        else if (!SoldiersAlive())
        {
            OnLose?.Invoke();
            EventOnLose?.Invoke();
            levelFinished = true;
        }


    }


    private void UpdateUICurrent(Text text, int value)
    {
        if (text)
            text.text = value.ToString();
    }

    private void UpdateUITotal()
    {
        if (ZombieTotal)
            ZombieTotal.text = Zombies.Count.ToString();

        if (SoldierTotal)
            SoldierTotal.text = Soldiers.Count.ToString();
    }

    private bool EnemiesAlive()
    {
        var enemiesAlive = Zombies.FindAll(zombie => zombie.IsAlive());

        if (ZombieCurrent)
            UpdateUICurrent(ZombieCurrent, enemiesAlive.Count);

        if (enemiesAlive.Count > 0)
            return true;
        else
            return false;
    }

    private bool SoldiersAlive()
    {
        var soldiersAlive = Soldiers.FindAll(soldier => soldier.IsAlive());


        if (SoldierCurrent)
            UpdateUICurrent(SoldierCurrent, soldiersAlive.Count);

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

        UpdateUITotal();
    }


}
