using System.Collections;
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
    // TODO: Separar ui em outro script
    public class LevelManager : Singleton<LevelManager>
    {
        public int targetFrameRate = 60;

        public Action OnWin;
        public Action OnLose;

        public UnityEvent EventOnWin;
        public UnityEvent EventOnLose;

        public List<string> enemiesTag = new List<string>();
        public List<string> alliesTag = new List<string>();

        public ObserverEvent ZombieDeathTag;
        public ObserverEvent SoldierDeathTag;

        private int soldierCount, zombieCount;

        [Title("UI")]
        [Title("Soldier")]
        public Text SoldierTotal;
        public Text SoldierCurrent;

        [Title("Zombie")]
        public Text ZombieTotal;
        public Text ZombieCurrent;

        [Button("Check Enemies")]
        private void BEnemiesAlive() => Debug.Log($"Enemies Alive: {zombieCount <= 0}");

        [Button("Check Allies")]
        private void BSoldiersAlive() => Debug.Log($"Allies Alive: {soldierCount <= 0}");

        private bool levelFinished;

        protected override void Awake()
        {
            base.Awake();
            CountCharacters();
            Application.targetFrameRate = targetFrameRate;
        }

        private void CountCharacters()
        {
            var allCharacters = FindObjectsOfType<Health>().ToList();
            zombieCount = allCharacters.FindAll(zombie => enemiesTag.Contains(zombie.tag)).Count;
            soldierCount = allCharacters.FindAll(soldiers => alliesTag.Contains(soldiers.tag)).Count;
            UpdateUITotal();
        }

        private void UpdateCountEntities(int zombieNewValue, int soldierNewValue)
        {
            zombieCount = zombieNewValue;
            soldierCount = soldierNewValue;

            UpdateUICurrent(SoldierCurrent, soldierCount);
            UpdateUICurrent(ZombieCurrent, zombieCount);

            CheckEndGame();
        }

        private void CheckEndGame()
        {
            if (levelFinished) return;

            if (zombieCount <= 0)
            {
                OnWin?.Invoke();
                EventOnWin?.Invoke();
                levelFinished = true;
            }
            else if (soldierCount <= 0)
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
                ZombieTotal.text = zombieCount.ToString();
            if (SoldierTotal)
                SoldierTotal.text = soldierCount.ToString();
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