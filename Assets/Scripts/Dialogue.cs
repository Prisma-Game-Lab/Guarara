using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue //Essa classe será passada para o script do DialogueManager como um GameObject
{
    public string name;
    public Sprite charProfile;
    [TextArea(3,10)]
    public string[] sentences;
}
