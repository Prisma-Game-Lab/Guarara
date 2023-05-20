using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryScript : MonoBehaviour
{
    public GameObject itemInvPrefab;
    public InventoryManager inventoryManager;
    
    public Color corFundoCaixa;
    public Color corCimaCaixa;
    public Color corSelecaoEdit;
    public Color corSelecaoMao;

    private GameObject gridObj;
    
    [SerializeField]
    private List<ActiveItens> activeItens = new List<ActiveItens>();

    public int indexSelecAtual;

    private Vector3 FirstPos;

    private int prevIndexSelected = -1;

    private void Start() 
    {
        gridObj = transform.Find("Grid").gameObject;

        for (int i = 0; i <= 17; i++)
        {
            GameObject itemObj = Instantiate(itemInvPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            itemObj.transform.SetParent(gridObj.transform, false);
            RectTransform rtOb = itemObj.GetComponent<RectTransform>();
            itemObj.name = "caixa" + i.ToString();
            itemObj.transform.Find("Cima").gameObject.GetComponent<Image>().color = corCimaCaixa;
            itemObj.transform.Find("Fundo").gameObject.GetComponent<Image>().color = corFundoCaixa;
            GameObject dBox = itemObj.transform.Find("DescriptionBox").gameObject;
            GameObject itemImage = itemObj.transform.Find("Image").gameObject;
            
            if(i < inventoryManager.ItensInv.Count)
            {
                itemImage.GetComponent<Image>().sprite = inventoryManager.ItensInv[i].image;
                dBox.transform.Find("Texto").gameObject.GetComponent<TextMeshProUGUI>().text = inventoryManager.ItensInv[i].description;
                dBox.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = inventoryManager.ItensInv[i].nameItem;

                ActiveItens novoItem = new ActiveItens();
                novoItem.objectItem = itemObj;
                novoItem.descriptionItem = itemObj.transform.Find("DescriptionBox").gameObject;
                novoItem.fundo = itemObj.transform.Find("Fundo").gameObject.GetComponent<Image>();

                activeItens.Add(novoItem);

                if(i == 0 || i == inventoryManager.indexHolding)
                {
                    ChangeBox(i);
                    indexSelecAtual = i;  
                }
                
            }else
            {
                itemImage.SetActive(false);
            }
        }
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            int calc = indexSelecAtual - 6;

            if(activeItens.Count != 0 && calc >= 0)
            {
                ChangeBox(calc);        
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            int calc = indexSelecAtual - 1;

            if(activeItens.Count != 0 && calc >= 0)
            {
                ChangeBox(calc);      
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            int calc = indexSelecAtual + 6;

            if(calc < activeItens.Count)
            {
                ChangeBox(calc);      
            } 
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            int calc = indexSelecAtual + 1;

            if(calc < activeItens.Count)
            {
                ChangeBox(calc);      
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryManager.ChangeHolding(indexSelecAtual);
        }

    }

    public void ChangeBox(int calc)
    {

        activeItens[indexSelecAtual].descriptionItem.SetActive(false);
        activeItens[indexSelecAtual].fundo.color = corFundoCaixa;
        indexSelecAtual = calc; 
        activeItens[indexSelecAtual].fundo.color = corSelecaoEdit;    
        activeItens[indexSelecAtual].descriptionItem.SetActive(true);

        int iHold = inventoryManager.indexHolding;

        if(iHold != -1 && iHold < activeItens.Count)
        {
            if(prevIndexSelected != -1)
            {
                activeItens[prevIndexSelected].fundo.color = corFundoCaixa;
            }

            activeItens[iHold].fundo.color = corSelecaoMao;
            prevIndexSelected = iHold;
        }
    }

}

[System.Serializable]
public class ActiveItens
{

    public GameObject objectItem;
    public Image fundo;
    public GameObject descriptionItem;

}
