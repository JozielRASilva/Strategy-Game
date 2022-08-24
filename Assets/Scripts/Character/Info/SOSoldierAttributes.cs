using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZombieDiorama.Character.Info
{
    [CreateAssetMenu(fileName = "SO_Soldier", menuName = "NPCs/Soldier", order = 1)]
    public class SOSoldierAttributes : SOAttributesBase
    {
        [TitleGroup("Regroup")]
        public float DistanceToRegroup = 1;

        [TitleGroup("Team")]
        public float DistanceToLeader = 1;

        [TitleGroup("Fight")]
        public float ShootCooldown = 0.5f;
        public float LookAtZombieDamping = 15f;

        [TitleGroup("Combat")]
        public Vector2 RangeToSeeTarget = new Vector2(5, 20);
    }
}