using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova Lista de Conversas", menuName = "Scriptable Objects/Conversas")]
public class CharacterDialogue : ScriptableObject
{
    public Conversation[] CharacterDialogues;
    
    public Conversation GetConversationByIndex (int indexConversation)
    {
        return CharacterDialogues[indexConversation];
    }
}
