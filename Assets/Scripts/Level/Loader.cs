using UnityEngine;

namespace ZombieDiorama.Level
{
    public class Loader : MonoBehaviour
    {
        public int BuildIndex;

        public void Load()
        {
            LevelLoaderManager.Load(BuildIndex);
        }
    }
}