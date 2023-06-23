using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // variáveis
    [SerializeField]
    private InventoryItem itemPrefab;
    [SerializeField]
    private RectTransform contentPanel;
    List<InventoryItem> listOfItems = new List<InventoryItem>();

    public void InitalizeInventory(int inventoryItems)
    {
        for (int i = 0; i < inventoryItems; i++)
        {
            InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);
        }
    }

    public void Show()
    {
        Debug.Log("Mostrou");
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("Demostrou");
        gameObject.SetActive(false);
    }

}
