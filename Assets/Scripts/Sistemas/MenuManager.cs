using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    [SerializeField] public string cenaInicial;
    [SerializeField] public string menuPrincipal;
    [SerializeField] private GameObject painelMenuPrincipal;
    [SerializeField] private GameObject painelAjustes;
    [SerializeField] private GameObject painelPause;
    [SerializeField] private GameObject painelConfig;
    [SerializeField] private GameObject canvasPause;
    private ScenesManager sceneLoader;

    //funções do menu inicial

    private void Start()
    {
        sceneLoader = GameObject.FindWithTag("SceneManager").GetComponent<ScenesManager>();
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

    //funções do menu de pausa ingame

    public void Pausar()
    {
        canvasPause.SetActive(true);
    }

    public void MenuPrincipal()
    {
        sceneLoader.GoToScene(menuPrincipal);
    }

    public void AbrirConfig()
    {
        painelPause.SetActive(false);
        painelConfig.SetActive(true);
    }

    public void FecharConfig()
    {
        painelConfig.SetActive(false);
        painelPause.SetActive(true);
    }

    public void RetomarJogo()
    {
        canvasPause.SetActive(false);
    }

}
