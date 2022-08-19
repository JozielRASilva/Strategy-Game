
using UnityEngine;
namespace ZombieDiorama.Extensions
{
    public static class UnityLayerMaskExtensions
    {
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}