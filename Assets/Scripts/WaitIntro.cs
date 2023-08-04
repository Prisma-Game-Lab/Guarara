using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitIntro : MonoBehaviour
{  
    private ScenesManager sceneLoader;

    void Start()
    {
        sceneLoader = GameObject.FindWithTag("SceneManager").GetComponent<ScenesManager>();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(7.5f);

        sceneLoader.GoToScene("Menu");
    }

}
