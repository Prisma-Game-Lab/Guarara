using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjAnalise : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string objSceneName;
    private ScenesManager sceneLoader;
    void Start()
    {
        sceneLoader = FindObjectOfType<ScenesManager>();
    }

    // ao interagir com o objeto o jogador troca de cena
    public void Interact()
    {
        Debug.Log("Mudou de cena");
        sceneLoader.GoToScene(objSceneName);
    }
}
