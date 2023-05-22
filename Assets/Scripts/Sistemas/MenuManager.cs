using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private string cenaInicial;
    [SerializeField] private GameObject painelMenuPrincipal;
    [SerializeField] private GameObject painelAjustes;
    ScenesManager sceneLoader;

    private void Start()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ScenesManager>();
    }
    public void Jogar()
    {
        sceneLoader.GoToScene(cenaInicial);
    }

    public void AbrirAjustes()
    {
        painelMenuPrincipal.SetActive(false);
        painelAjustes.SetActive(true);
    }

    public void FecharAJustes()
    {
        painelAjustes.SetActive(false);
        painelMenuPrincipal.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }


}
