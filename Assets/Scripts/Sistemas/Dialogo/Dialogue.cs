using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject playerConfig;
    [SerializeField]
    private GameObject dialogPrefab;
    [SerializeField]
    private float yPosAdd;
    private GameObject dialogObject;

    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private string NomePersonagem;
    [SerializeField]
    private Sprite SpritePersonagem;

    [SerializeField]
    private int dialogoAtual;

    private Transform canvasTransform; 

    private bool podeIniciarD;
    private bool clickedNext;
    
    private GameObject dialogueBoxAtual;

    public List<DialogueList> listaDialogo = new List<DialogueList>();

    private void Start() 
    {
        canvasTransform = GameObject.Find("Canvas").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 pos = Vector3.up;
            pos.y += yPosAdd;

            if (dialogObject == null)
            {
                dialogObject = Instantiate(dialogPrefab, transform.position + pos, Quaternion.identity);
            }

            podeIniciarD = true;
            StartCoroutine(WaitingClick());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (dialogObject != null)
            {
                Destroy(dialogObject);
                dialogObject = null;
            }

            if(dialogueBoxAtual != null)
            {
                Destroy(dialogueBoxAtual);
                dialogueBoxAtual = null;   
            }

            podeIniciarD = false;
        }
    }

    private IEnumerator WaitingClick()
    {
        while (podeIniciarD)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogo();
                Destroy(dialogObject);
                dialogObject = null;
                podeIniciarD = false;
            }

            yield return null;
        }

    }

    private void dialogo()
    {
        Vector3 pos = new Vector3(0f, 0f, 0f);
        var dialogueBoxOBJ = Instantiate(dialogueBox, pos, Quaternion.identity, canvasTransform);
        dialogueBoxAtual = dialogueBoxOBJ;
        dialogueBoxOBJ.GetComponent<RectTransform>().anchoredPosition = pos;
        DialogueBoxScpt dialogueBoxScript = dialogueBoxOBJ.GetComponentInChildren<DialogueBoxScpt>();
        dialogueBoxScript.dialogue = this;
        dialogueBoxScript.nomemc = playerConfig.name;
        dialogueBoxScript.spritemc = playerConfig.GetComponent<SpriteRenderer>().sprite;
        dialogueBoxScript.nomeperso = NomePersonagem;
        dialogueBoxScript.spriteperso = SpritePersonagem;
        dialogueBoxScript.dialogoAtual = dialogoAtual;
    }

}

public enum CharacterType
{
        Player,
        NPC
}

public enum TextSpeed
{
    Medio,
    Rapido,
    Lento
}

[System.Serializable]
public class DialogueList
{

public List<DialogueIF> inFactDialogue = new List<DialogueIF>();

}

[System.Serializable]
public class DialogueIF
{

public CharacterType whoIsTalking;

public string dialogue;

public TextSpeed textSpeed;

}
