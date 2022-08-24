using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using ZombieDiorama.Utilities.Patterns;

namespace ZombieDiorama.Level
{
    public class LevelLoaderManager : Singleton<LevelLoaderManager>
    {
        [FormerlySerializedAs("animator")] public Animator Animator;
        [FormerlySerializedAs("triggerName")] public string TriggerName = "start";
        [FormerlySerializedAs("transitionTime")] public float TransitionTime = 2;
        [FormerlySerializedAs("nextLevel")] public string NextLevel;
        [FormerlySerializedAs("loadSlider")] public Slider LoadSlider;

        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevelCO(NextLevel));
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
            Animator.SetTrigger(TriggerName);

            yield return new WaitForSeconds(TransitionTime);

            SceneManager.LoadScene(levelName);
        }

        private IEnumerator LoadLevelCO(int levelIndex)
        {
            Animator.SetTrigger(TriggerName);

            yield return new WaitForSeconds(TransitionTime);

            StartCoroutine(LoadAsynchrously(levelIndex));
        }

        private IEnumerator LoadAsynchrously(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            LoadSlider.gameObject.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                LoadSlider.value = progress;
                yield return null;
            }
        }
    }
}