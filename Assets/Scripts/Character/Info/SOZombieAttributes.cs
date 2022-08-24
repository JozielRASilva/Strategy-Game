using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using ZombieDiorama.Utilities.Primitives;

namespace ZombieDiorama.Character.Info
{
    [CreateAssetMenu(fileName = "SO_Zombie", menuName = "NPCs/Zombie", order = 3)]
    public class SOZombieAttributes : SOAttributesBase
    {
        [TitleGroup("Patrol")]
        public float DistanceToPatrolTarget;
        public float DistanceToWaypoint;

        [TitleGroup("Combat")]
        public float CoolDown;

        [TitleGroup("Call Zombies")]
        public float ListeningField;
        public float DistanceToZombie;
        public float TimeCalling;
        public SOString CallCounterTag;

        [TitleGroup("Chasing Soldier FOV")]
        public float MinDistance;
        public float MaxDistance;
    }
}