using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    public GameObject continueButton;
    public Image defaultImage;
    private int index;

    public Queue<string> sentences;
    private Conversation currentConversation;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        defaultImage.sprite = dialogue.charProfile;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            NextDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        // Debug.Log(sentence);

    }
    public void EndConversation()
    {
        continueButton.SetActive(false);
        dialogueBox.SetActive(false);
    }

    public void StartConversation(Conversation conversation)
    {
        index = 0;
        currentConversation = conversation;
        dialogueBox.SetActive(true);
        continueButton.SetActive(true);
        StartDialogue(currentConversation.GetDialogueByIndex(index));
        index++;
    }
    private void NextDialogue()
    {
        if (index < currentConversation.GetDialogues().Length)
        {
            StartDialogue(currentConversation.GetDialogueByIndex(index));
            index++;
            return;
        }
        EndConversation();
    }


}
