using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxScpt : MonoBehaviour
{

    public Dialogue dialogue;
    
    public int dialogoAtual;
    public string nomemc;
    public string nomeperso;

    private TextMeshProUGUI dText;
    private TextMeshProUGUI cNome; 

    private bool clickedNext;

    private string fullText;
    private string currentText;

    bool dialogoAtivo = true;

    float textSpeedAC;

    private void Start() 
    {
        dText = transform.Find("Dtext").GetComponent<TextMeshProUGUI>();
        cNome = transform.Find("Cname").GetComponent<TextMeshProUGUI>();
        StartCoroutine(dialogo());
    }
    
    private void Update() 
    {
        if(dialogoAtivo && textSpeedAC > 0.0001)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                textSpeedAC = textSpeedAC/2;
            } 
        }   
    }

    private IEnumerator WaitingClick()
    {

        while(!clickedNext)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    clickedNext = true;
                }

                yield return null;
            }

    }

    private IEnumerator dialogo()
    {

        for (int i = 0; i < dialogue.listaDialogo[dialogoAtual].inFactDialogue.Count; i++)
        {
            dialogoAtivo = true;

            if(dialogue.listaDialogo[dialogoAtual].inFactDialogue[i].whoIsTalking == CharacterType.Player)
            {
                cNome.text = nomemc;
            }else
            {
                cNome.text = nomeperso;     
            }
        
            fullText = dialogue.listaDialogo[dialogoAtual].inFactDialogue[i].dialogue;
            currentText = "";

            float delay = GetDelay(dialogue.listaDialogo[dialogoAtual].inFactDialogue[i].textSpeed);
            textSpeedAC = delay;            
            yield return StartCoroutine(DisplayTextGradually(i));

            clickedNext = false;

            dialogoAtivo = false;
            yield return StartCoroutine(WaitingClick());

            
        }

        Destroy(this.gameObject);
    }

    private IEnumerator DisplayTextGradually(int index)
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);

            dText.text = currentText;

            yield return new WaitForSeconds(textSpeedAC);
        }
    }

    private float GetDelay(TextSpeed textSpeed)
    {
        switch (textSpeed)
        {
            case TextSpeed.Rapido:
                return 0.01f;
            case TextSpeed.Medio:
                return 0.05f;
            case TextSpeed.Lento:
                return 0.1f;
            default:
                return 0.05f; 
        }
    }
}