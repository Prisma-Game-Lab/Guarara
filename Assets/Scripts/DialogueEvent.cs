using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEvent : MonoBehaviour
{
   public UnityEvent DialogueStart;
   

   public void StartConversation ()
    {
        if(GetComponent<IsNearEvent>().wasCollided == true && GetComponent<IsNearEvent>().characterTag=="NPC")
        {
            DialogueStart?.Invoke();
            Debug.Log("começar diálogo!");
        }
    }
   void Update ()
    {
        if(Input.GetMouseButtonDown(1))
        {
            StartConversation();    
        }
    }

}
