using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private string cenaInicial;
    [SerializeField] private GameObject painelMenuPrincipal;
    [SerializeField] private GameObject painelAjustes;


    public void Jogar()
    {
        SceneManager.LoadScene(cenaInicial);
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
