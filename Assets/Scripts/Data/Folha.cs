using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova folha", menuName = "Scriptable Objects/Folha")]
public class Folha : ScriptableObject
{
    public string nome;
    public Sprite folhaDiário;
    public int position;
    public int idade;
    public string ocupação;
}
