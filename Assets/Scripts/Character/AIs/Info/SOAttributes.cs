using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ZombieDiorama.Character.AIs.Info
{
    [CreateAssetMenu(fileName = "NPC", menuName = "NPCs/NPC", order = 1)]
    public class SOAttributes : ScriptableObject
    {
        public Color color = Color.white;

        public float speed = 1;
        public string target = "Coin";
        public float distance = 1;
        public float distanceToCollect = 1;

        [Title("Combat")]
        public string enemy = "NPC";
        public float rangeToCheckEnemy = 5;
        public GameObject projectile;

        [Title("Dodge")]
        public string dodgeThis = "Projectile";
        public float coolDown = 2;
        public Vector2 dodgeTimeRange = new Vector2(1, 2);
    }
}