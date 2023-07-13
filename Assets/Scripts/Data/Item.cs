using UnityEngine;

[CreateAssetMenu(fileName = "Novo item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    public string itemDescription;
}
