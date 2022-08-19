using System.Collections;

namespace ZombieDiorama.Character
{
    public interface IKillableObject
    {
        IEnumerator DestroyCO();
        void Kill();
    }
}