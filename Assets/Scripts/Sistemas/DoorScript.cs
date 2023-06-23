using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // variáveis
    [SerializeField]
    private string nextSceneName;
    private ScenesManager sceneLoader;
    void Start()
    {
        sceneLoader = FindObjectOfType<ScenesManager>();
    }

    // troca de cena quando o jogador passa por uma porta
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            sceneLoader.GoToScene(nextSceneName);
        }
    }
}
