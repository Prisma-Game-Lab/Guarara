using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEvent : MonoBehaviour
{
   public UnityEvent OnRightButtonPressed;
   

   public void StartConversation ()
    {
        if(GetComponent<IsNearEvent>().wasCollided == true && GetComponent<IsNearEvent>().characterTag=="NPC")
        {
            OnRightButtonPressed?.Invoke();
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
