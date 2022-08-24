using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZombieDiorama.Level
{
    public class LevelReset : MonoBehaviour
    {
        public void ReStart()
        {
            LevelLoaderManager.Load(SceneManager.GetActiveScene().buildIndex);
        }
    }
}