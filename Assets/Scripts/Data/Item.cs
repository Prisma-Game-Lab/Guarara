using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Novo item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
}
