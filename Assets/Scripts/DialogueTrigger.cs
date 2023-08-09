using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public CharacterDialogue characterDialogues;
    [Tooltip("Marcar quando o diálogo começar automaticamente assim que a cena carrega.")]
    public bool isAutomatic;
    public bool isFirstTime = true;

    public void OnStartConversation()
    {
        Conversation conversation = characterDialogues.GetConversationByIndex(GetComponent<UpdateDiary>().indexNecessaItems);
        FindObjectOfType<DialogueManager>().StartConversation(conversation);
        isFirstTime = false;
    }

    void Start()
    {
        if (isAutomatic)
        {
            OnStartConversation();
        }
    }
}
