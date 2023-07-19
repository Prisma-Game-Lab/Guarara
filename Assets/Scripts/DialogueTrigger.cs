using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Conversation conversation;

    public void OnStartConversation ()
    {
        FindObjectOfType<DialogueManager>().StartConversation(conversation);
        
    }
}
