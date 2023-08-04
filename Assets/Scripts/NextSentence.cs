using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSentence : MonoBehaviour, IInteractable
{
    [SerializeField]
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
    }

    public void Interact()
    {
        dialogueManager.DisplayNextSentence();
    }
}
