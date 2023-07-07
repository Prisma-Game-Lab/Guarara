using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbreObjAnalise : MonoBehaviour, IInteractable
{
    // variáveis
    [SerializeField]
    private GameObject objeto;
    [SerializeField]
    private bool aberto = false;
    private GameObject player;
    private PlayerControl playerScript;
    private GameObject canvas;

    private void Update()
    {
        if (!objeto.activeInHierarchy)
        {
            playerScript.analisando = false;
            aberto = false;
        }
    }
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerControl>();
        canvas = GameObject.Find("Canvas");
        objeto = this.gameObject.transform.GetChild(0).gameObject;
    }


    // ao interagir com o objeto o jogador abre a cena
    public void Interact()
    {
        if (!aberto)
        {
            objeto.SetActive(true);
            aberto = true;
            playerScript.analisando = true;
            playerScript.SwitchActions();
        }
    }
}
