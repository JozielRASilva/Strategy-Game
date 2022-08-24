using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Level;

namespace ZombieDiorama.UI.Level
{
    public class UIZombieCounterDisplayer : UIFractionCounterDisplayer
    {
        protected override void Subscribe()
        {
            LevelManager.OnInitZombiesCount += UpdateTotal;
            LevelManager.OnUpdateZombiesCount += UpdateCurrent;
        }

        protected override void UnSubscribe()
        {
            LevelManager.OnInitZombiesCount -= UpdateTotal;
            LevelManager.OnUpdateZombiesCount -= UpdateCurrent;
        }
    }
}