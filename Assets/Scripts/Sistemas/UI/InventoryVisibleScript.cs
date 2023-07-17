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

    // Update is called once per frame
    void Update()
    {
        ListItems();
    }
    // faz o L
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // desfaz o L
    public void Hide()
    {
        gameObject.SetActive(false);
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
