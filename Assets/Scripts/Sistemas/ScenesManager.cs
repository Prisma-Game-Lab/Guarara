using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float time = 1f;

    // recebe o nome da cena a ser carregada
    public void GoToScene(string nextSceneName)
    {
        StartCoroutine(LoadLevelTransition(nextSceneName));
    }

    // começa a animação de transição
    IEnumerator LoadLevelTransition(string SceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneName);
    }
}
