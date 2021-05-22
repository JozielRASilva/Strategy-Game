using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{

    public Animator animator;

    public string triggerName = "start";

    public float transitionTime = 2;

    public string nextLevel;

    public Slider loadSlider;


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(nextLevel));
    }

    public void LoadNextLevel(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(string levelName)
    {

        animator.SetTrigger(triggerName);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }


    IEnumerator LoadLevel(int levelIndex)
    {

        animator.SetTrigger(triggerName);

        yield return new WaitForSeconds(transitionTime);

        StartCoroutine(LoadAsynchrously(levelIndex));
    }

    IEnumerator LoadAsynchrously(int sceneIndex)
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
