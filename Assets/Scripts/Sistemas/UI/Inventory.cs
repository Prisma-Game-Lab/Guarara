using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory : MonoBehaviour
{
    // variáveis
    [SerializeField]
    private Transform contentPanel;
    [SerializeField]
    private TMP_Text descriptionPanel;

    [SerializeField]
    List<Item> listOfItems = new List<Item>();

    private void Update()
    {
        ShowDescription();
    }

    public void AddItem(Item item)
    {
        listOfItems.Add(item);
    }

    public void RemoveItem(Item item)
    {
        listOfItems.Remove(item);
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
        foreach (var item in listOfItems)
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

    // mostra a descrição dos itens
    public void ShowDescription()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        int index = int.Parse(selected.name[selected.name.Length - 1].ToString());
        if (index < listOfItems.Count)
        {
            descriptionPanel.text = listOfItems[int.Parse(selected.name[selected.name.Length - 1].ToString())].itemDescription;
        }
        else
        {
            descriptionPanel.text = "";
        }
    }
}
