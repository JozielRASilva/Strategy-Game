using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using ZombieDiorama.Character;
using ZombieDiorama.Utilities.Patterns;

namespace ZombieDiorama.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        public List<string> enemiesTag = new List<string>();
        public List<string> alliesTag = new List<string>();

        public ObserverEvent ZombieDeathTag;
        public ObserverEvent SoldierDeathTag;

        public static Action OnWin;
        public static Action OnLose;

        public static Action<int> OnInitZombiesCount;
        public static Action<int> OnInitSoldiersCount;

        public static Action<int> OnUpdateZombiesCount;
        public static Action<int> OnUpdateSoldiersCount;

        [Button("Check Enemies")]
        private void BEnemiesAlive() => Debug.Log($"Enemies Alive: {zombieCount <= 0}");

        [Button("Check Allies")]
        private void BSoldiersAlive() => Debug.Log($"Allies Alive: {soldierCount <= 0}");

        private bool levelFinished;
        private int soldierCount, zombieCount;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            CountCharacters();
        }

        private void CountCharacters()
        {
            var allCharacters = FindObjectsOfType<Health>().ToList();
            zombieCount = allCharacters.FindAll(zombie => enemiesTag.Contains(zombie.tag)).Count;
            soldierCount = allCharacters.FindAll(soldiers => alliesTag.Contains(soldiers.tag)).Count;

            OnInitZombiesCount?.Invoke(zombieCount);
            OnInitSoldiersCount?.Invoke(soldierCount);
        }

        private void UpdateCountEntities(int zombieNewValue, int soldierNewValue)
        {
            zombieCount = zombieNewValue;
            soldierCount = soldierNewValue;

            OnUpdateZombiesCount?.Invoke(zombieCount);
            OnUpdateSoldiersCount?.Invoke(soldierCount);

            CheckEndGame();
        }

        private void CheckEndGame()
        {
            if (levelFinished) return;

            if (zombieCount <= 0)
            {
                OnWin?.Invoke();
                levelFinished = true;
            }
            else if (soldierCount <= 0)
            {
                OnLose?.Invoke();
                levelFinished = true;
            }
        }

        private void OnEnable()
        {
            Observer.Subscribe(OnEntitiesChange);
        }

        private void OnDisable()
        {
            Observer.UnSubscribe(OnEntitiesChange);
        }

        private void OnEntitiesChange(ObserverEvent eventType, object o = null)
        {
            if (!eventType)
                return;

            if (eventType.IsValid(ZombieDeathTag))
            {
                UpdateCountEntities(zombieCount - 1, soldierCount);
            }
            else if (eventType.IsValid(SoldierDeathTag))
            {
                UpdateCountEntities(zombieCount, soldierCount - 1);
            }
        }

    }
}