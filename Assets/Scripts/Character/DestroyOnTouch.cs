using UnityEngine;
using Sirenix.OdinInspector;
using ZombieDiorama.Extensions;

namespace ZombieDiorama.Character
{
    public class DestroyOnTouch : KillableObject
    {
        [Title("Destroy Info")]
        public LayerMask Destructor;

        private void OnTriggerEnter(Collider other)
        {
            if (UnityLayerMaskExtensions.Contains(Destructor, other.gameObject.layer))
            {
                Destroy();
            }
        }
    }
}