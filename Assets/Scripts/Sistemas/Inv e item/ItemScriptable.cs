using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScriptable : MonoBehaviour
{
    [SerializeField]
    InventoryManager inventoryManager;
    [SerializeField]
    private int keyIndex;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private string nameItem;
    [SerializeField]
    private string description;
    [SerializeField]
    private bool isItemDestroyable;
    [SerializeField]
    private int uses;
    [SerializeField]
    private Color corTextoPopUp;

    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private float yPosAdd;


    private GameObject itemObject;

    private bool podeIniciarD;

    private void Start() 
    {
        inventoryManager = GameObject.Find("Sistemas/Inventory Manager").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 pos = Vector3.up;
            pos.y += yPosAdd;

            if (itemObject == null)
            {
                itemObject = Instantiate(itemPrefab, transform.position + pos, Quaternion.identity);
            }

            podeIniciarD = true;
            StartCoroutine(WaitingClick());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (itemObject != null)
            {
                Destroy(itemObject);
                itemObject = null;
            }

            podeIniciarD = false;
        }
    }

    private IEnumerator WaitingClick()
    {
        while (podeIniciarD)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ItensInv item = new ItensInv();
                item.keyIndex = keyIndex;
                item.image = image;
                item.nameItem = nameItem;
                item.description = description;
                item.isItemDestroyable = isItemDestroyable;
                item.uses = uses;
                item.corTextoPopUp = corTextoPopUp;

                inventoryManager.addIndexPopUp(inventoryManager.ItensInv.   Count);

                inventoryManager.ItensInv.Add(item);

                Destroy(this.gameObject);
            }

            yield return null;
        }

    }


}
