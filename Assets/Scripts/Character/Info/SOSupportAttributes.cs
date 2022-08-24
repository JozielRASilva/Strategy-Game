using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZombieDiorama.Character.Info
{
    [CreateAssetMenu(fileName = "SO_SupportSoldier", menuName = "NPCs/SupportSoldier", order = 2)]
    public class SOSupportAttributes : ScriptableObject
    {
        [TitleGroup("Set Object")]
        public float DistanceToSet = 0.5f;
        public float DelayToSet = 0.2f;

        [TitleGroup("Heal")]
        public float DistanceToHeal = 0.7f;
        public float DampingToHeal = 1f;

        public float HealCooldown = 2f;
        public float HealRest = 1f;
    }
}