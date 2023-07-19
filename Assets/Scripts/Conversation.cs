using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova Conversa", menuName = "Scriptable Objects/Conversa")]
public class Conversation : ScriptableObject
{
    public Dialogue[] dialogues;

    public Dialogue[] GetDialogues()
    {
        return dialogues;
    }

    public Dialogue GetDialogueByIndex (int index)
    {
        return dialogues[index];
    }
}
