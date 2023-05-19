using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpScpt : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public int index;
    public Sprite imagem;
    public string nome;
    public Color cor;

    private void Start() 
    {
        TextMeshProUGUI txt = gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        txt.text = nome;
        txt.color = cor;
        gameObject.transform.Find("Imagem").GetComponent<Image>().sprite = imagem;
    }

    public void Destroy()
    {
        inventoryManager.popUpEnded(index);
        Destroy(this.gameObject);
    }
}
