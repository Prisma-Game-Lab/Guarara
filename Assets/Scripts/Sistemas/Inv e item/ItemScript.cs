using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, IInteractable
{
    // variáveis
    public Item item;
    [SerializeField]
    private InventoryItems inventoryScriptObj;

    public void PickUp()
    {
        inventoryScriptObj.list.Add(item);
        Destroy(gameObject);
    }

    public void Interact()
    {
        PickUp();
        AudioManager.instance.PlaySfx("item");
    }
}
