using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Conversation conversation;
    [Tooltip("Marcar quando o diálogo começar automaticamente assim que a cena carrega.")]
    public bool isAutomatic;

    public void OnStartConversation ()
    {
        FindObjectOfType<DialogueManager>().StartConversation(conversation);
        
    }

    void Start()
    {
        if(isAutomatic)
        {
            OnStartConversation();
        }
    }
}
