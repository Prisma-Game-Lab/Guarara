using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, IInteractable
{
    // variáveis
    public Item item;
    [SerializeField]
    private Inventory inventory;

    void PickUp()
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }

    public void Interact()
    {
        PickUp();
    }
}
