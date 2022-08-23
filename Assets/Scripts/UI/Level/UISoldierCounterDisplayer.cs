using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDiorama.Level;

namespace ZombieDiorama.UI.Level
{
    public class UISoldierCounterDisplayer : UIFractionCounterDisplayer
    {
        protected override void Subscribe()
        {
            LevelManager.OnInitSoldiersCount += UpdateTotal;
            LevelManager.OnUpdateSoldiersCount += UpdateCurrent;
        }

        protected override void UnSubscribe()
        {
            LevelManager.OnInitSoldiersCount -= UpdateTotal;
            LevelManager.OnUpdateSoldiersCount -= UpdateCurrent;
        }
    }
}