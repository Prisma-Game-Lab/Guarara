using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouching : MonoBehaviour
{
    // checa se a mão tá encostando no objeto na cena de análise e mostra o diálogo caso esteja
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.GetComponent<DialogueTrigger>().OnStartConversation();
            Debug.Log("encostando");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<DialogueManager>().EndConversation();
        }
    }
}
