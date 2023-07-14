using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    public GameObject continueButton;

    public Queue<string> sentences;
    
    void Start()
    {
        sentences = new Queue<string>();
    }

   public void StartDialogue (Dialogue dialogue)
   {
        dialogueBox.SetActive(true);
        continueButton.SetActive(true);     

        Debug.Log("Começando diálogo com "+ dialogue.name);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
   }
   public void DisplayNextSentence ()
   {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);

   }
   void EndDialogue ()
   {
        Debug.Log("Fim do diálogo!");
        continueButton.SetActive(false);
        dialogueBox.SetActive(false);   
   }
 
}
