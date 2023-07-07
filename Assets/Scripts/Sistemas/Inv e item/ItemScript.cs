using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, IInteractable
{
    // variáveis
    public Item item;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private GameObject popUp;
    private GameObject objPopUp;
    private void Awake()
    {
        inventory = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(0).GetComponent<Inventory>();
    }

    void PickUp()
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }

    public void Interact()
    {
        PickUp();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objPopUp = Instantiate(popUp, transform.position + Vector3.up, Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(objPopUp);
        }
    }
}
