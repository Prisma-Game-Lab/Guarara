using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryVisibleScript : MonoBehaviour
{
    // variáveis
    [SerializeField]
    private Transform contentPanel;
    [SerializeField]
    private InventoryItems scriptObj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ListItems();
    }

    public void AddItem(Item item)
    {
        scriptObj.list.Add(item);
    }

    public void RemoveItem(Item item)
    {
        scriptObj.list.Remove(item);
    }

    // mostra os itens em cada slot
    public void ListItems()
    {
        int index = 0;
        foreach (var item in scriptObj.list)
        {
            Image itemIcon = contentPanel.GetChild(index).transform.Find("ItemIcon").GetComponent<Image>();
            Text itemName = contentPanel.GetChild(index).transform.Find("ItemName").GetComponent<Text>();

            itemIcon.enabled = true;
            // itemName.enabled = true;

            itemIcon.sprite = item.itemIcon;
            // itemName.text = item.itemName;
            index++;
        }
    }
}
