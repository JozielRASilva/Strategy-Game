using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZombieDiorama.Utilities.Patterns;

namespace ZombieDiorama.Level
{
    public class LevelLoaderManager : Singleton<LevelLoaderManager>
    {
        public Animator animator;
        public string triggerName = "start";
        public float transitionTime = 2;
        public string nextLevel;
        public Slider loadSlider;

        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevelCO(nextLevel));
        }

        public static void Load(int levelIndex)
        {
            Instance?.LoadLevel(levelIndex);
        }

        public void LoadLevel(int levelIndex)
        {
            StartCoroutine(LoadLevelCO(levelIndex));
        }

        private IEnumerator LoadLevelCO(string levelName)
        {
            animator.SetTrigger(triggerName);

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(levelName);
        }

        private IEnumerator LoadLevelCO(int levelIndex)
        {
            animator.SetTrigger(triggerName);

            yield return new WaitForSeconds(transitionTime);

            StartCoroutine(LoadAsynchrously(levelIndex));
        }

        private IEnumerator LoadAsynchrously(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            loadSlider.gameObject.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                loadSlider.value = progress;
                yield return null;
            }
        }
    }
}