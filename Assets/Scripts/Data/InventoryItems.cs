using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Items", menuName = "Scriptable Objects/Inventory Items")]
public class InventoryItems : ScriptableObject
{
    public List<Item> list = new List<Item>();
}
