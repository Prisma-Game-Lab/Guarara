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
    private InventoryItems scriptObj;
    private List<Transform> inventorySlots = new List<Transform>();
    private Transform content;
    private void Awake()
    {
        // pega todos os slots do inventário e coloca em uma lista
        content = this.transform.GetChild(0).transform.GetChild(0).transform;
        foreach (Transform child in content)
        {
            inventorySlots.Add(child);
        }
    }

    private void Update()
    {
        ShowDescription();
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

            itemIcon.enabled = true;

            itemIcon.sprite = item.itemIcon;
            index++;
        }
    }

    // mostra a descrição dos itens
    private void ShowDescription()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        int index = inventorySlots.IndexOf(selected.transform);
        if (index < scriptObj.list.Count)
        {
            descriptionPanel.text = scriptObj.list[index].itemDescription;
        }
        else
        {
            descriptionPanel.text = "";
        }
    }
}
