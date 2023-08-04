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
        if (GetComponent<IsNearEvent>().wasCollided == true && GetComponent<IsNearEvent>().characterTag == "NPC")
        {
            OnEKeyPressed?.Invoke();
        }
    }
    public void Interact()
    {
        StartConversation();
    }

    //    void Awake()
    //    {
    //         action.FindActionMap("Player").FindAction("Interact").performed += OnEPressed;

    //    }
}
