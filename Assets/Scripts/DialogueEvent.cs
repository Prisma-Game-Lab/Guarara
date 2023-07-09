using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueEvent : MonoBehaviour
{
   public UnityEvent DialogueStart;
   [SerializeField] public string[] NPCNames;
   public bool isInList = false;
   
   private PlayerInput gameActions;
   private InputAction rightButtonPressAction;
   private bool wasRightPressed = false;
   

   public void StartConversation ()
    {
        if(gameObject.GetComponent<IsNearEvent>().wasCollided == true && checkList()==true)
        {
            if(wasRightPressed)
            {
                DialogueStart?.Invoke();
            }
        }
    }
    public bool checkList() //Verifica se o NPC existe na lista de NPCs que carregam informações.
    {
        name = gameObject.GetComponent<IsNearEvent>().characterName;
        for(int i = 0; i < NPCNames.Length; i++)
            {
                if(name==NPCNames[i])
                {
                    isInList = true;
                }
                isInList = false;
            }
        return isInList;
    }
    private void Awake () //O diálogo só começa se o botão direito mouse é clicado
    {
        gameActions = new PlayerInput();
        rightButtonPressAction = gameActions.Player.Interact;

        rightButtonPressAction.performed += context => wasRightPressed = true;
        rightButtonPressAction.canceled += context => wasRightPressed = false;
    
    }

}
