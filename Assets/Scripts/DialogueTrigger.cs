using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void OnStartConversation ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
