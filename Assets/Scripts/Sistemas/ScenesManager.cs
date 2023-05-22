using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private Animator transition;
    [SerializeField] private float time = 1f;

    public void GoToScene(string nextSceneName)
    {
        StartCoroutine(LoadLevelTransition(nextSceneName));
    }

    IEnumerator LoadLevelTransition(string SceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneName);
    }
}
