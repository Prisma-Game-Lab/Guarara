using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class DialogueEvent : MonoBehaviour, IInteractable
{
    public UnityEvent OnEKeyPressed;
    public InputActionAsset action;
    private InputAction interactionAction;

    public void StartConversation()
    {
        if (GetComponent<IsNearEvent>().wasCollided == true && GetComponent<IsNearEvent>().characterTag == "Player")
        {
            OnEKeyPressed?.Invoke();
        }
    }
    public void Interact()
    {
        StartConversation();
    }
    /* void Update ()
     {
         if(Input.GetMouseButtonDown(1))
         {
             StartConversation();    
         }
     } */
    //    void Awake()
    //    {
    //         action.FindActionMap("Player").FindAction("Interact").performed += OnEPressed;
    //    }
}
