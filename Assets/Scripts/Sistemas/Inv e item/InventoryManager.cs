using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<ItensInv> ItensInv = new List<ItensInv>();

    public List<int> indexPopUp = new List<int>();

    private bool canDisplay = true;

    public bool isInvActive = false;

    [SerializeField]
    private GameObject inventarioPrefab;
    [SerializeField]
    private GameObject popUpPrefab;
    [SerializeField]
    private GameObject itemInvPrefab;

    [SerializeField]
    private GameObject itemHandPrefab;

    [SerializeField]
    private Vector3 itemHandPos;
    
    private GameObject handImageObj;
    private Image handImage;

    private GameObject objPai;

    private GameObject inventarioObject;
    private GameObject itemHandObject;

    [SerializeField]
    private Color corFundoCaixa;
    [SerializeField]
    private Color corCimaCaixa;
    [SerializeField]
    private Color corSelecaoEdit;
    [SerializeField]
    private Color corSelecaoMao;
    
    public int indexHolding = -1;

    private InventoryScript inventarioObjectScript;

    private void Start() 
    {

        objPai = GameObject.Find("Canvas");  
        
        GameObject iHandObj = Instantiate(itemHandPrefab, transform.position, Quaternion.identity); 
        iHandObj.transform.SetParent(objPai.transform, false); 
        iHandObj.GetComponent<RectTransform>().anchoredPosition = itemHandPos;
        iHandObj.transform.Find("Cima").gameObject.GetComponent<Image>().color = corCimaCaixa;
        iHandObj.transform.Find("Fundo").gameObject.GetComponent<Image>().color = corFundoCaixa;

        itemHandObject = iHandObj;

        handImageObj = itemHandObject.transform.Find("Image").gameObject;
        handImage = itemHandObject.transform.Find("Image").GetComponent<Image>();
    }

    private void Update()
    {
        if(indexPopUp.Count != 0)
        {
            if(canDisplay)
            {
                DisplayPopUp(indexPopUp[0]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(isInvActive)
            {
                Destroy(inventarioObject);
                inventarioObject = null;
                isInvActive = false;
            }else
            {
                CreateInventory();
                isInvActive = true;
            }
        }
    }

    public void addIndexPopUp(int index)
    {
        indexPopUp.Add(index);
    }

    private void DisplayPopUp(int index)
    {
        canDisplay = false;

        GameObject PopUpObj = Instantiate(popUpPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        PopUpObj.transform.SetParent(objPai.transform, false); 
        PopUpScpt PopUpObjScpt = PopUpObj.GetComponent<PopUpScpt>();
        PopUpObjScpt.inventoryManager = this;
        PopUpObjScpt.index = index;
        PopUpObjScpt.imagem = ItensInv[index].image;
        PopUpObjScpt.nome = ItensInv[index].nameItem;
        PopUpObjScpt.cor =  ItensInv[index].corTextoPopUp;
    }

    public void popUpEnded(int index)
    {
        for (int i = 0; i < indexPopUp.Count; i++)
        {
            canDisplay = true;
            if(indexPopUp[i] == index)
            {
                indexPopUp.RemoveAt(i);
            }
        }
    }

    private void CreateInventory()
    {
        Vector3 pos = new Vector3(0f, 0f, 0f);
        inventarioObject = Instantiate(inventarioPrefab, transform.position + pos, Quaternion.identity);
        inventarioObject.GetComponent<RectTransform>().anchoredPosition = pos;
        inventarioObject.transform.SetParent(objPai.transform, false);
        inventarioObjectScript = inventarioObject.GetComponent<InventoryScript>();
        inventarioObjectScript.itemInvPrefab = itemInvPrefab;
        inventarioObjectScript.inventoryManager = this;
        inventarioObjectScript.corFundoCaixa = corFundoCaixa;
        inventarioObjectScript.corCimaCaixa = corCimaCaixa;
        inventarioObjectScript.corSelecaoEdit = corSelecaoEdit;
        inventarioObjectScript.corSelecaoMao = corSelecaoMao;
    }

    public void ChangeHolding(int index)
    {
        if(index == -1)
        {
            indexHolding = -1;
            handImageObj.SetActive(false);
        }
        else if(index != indexHolding)
        {
            handImageObj.SetActive(true);
            handImage.sprite = ItensInv[index].image;
            indexHolding = index;
            inventarioObjectScript.ChangeBox(inventarioObjectScript.indexSelecAtual);
        }
        else
        {
            indexHolding = -1;
            handImageObj.SetActive(false);
            inventarioObjectScript.ChangeBox(inventarioObjectScript.indexSelecAtual);
        }
    }

    public void itemUsed(int indexKey)
    {
        for (int i = 0; i < ItensInv.Count; i++)
        {
            if(ItensInv[i].keyIndex == indexKey)
            {
                ItensInv[i].uses -= 1;

                if(ItensInv[i].uses == 0)
                {
                    indexHolding = -1;
                    ItensInv.RemoveAt(i);
                    ChangeHolding(-1);
                    recalculatePopUpIndex(i);
                }
            }
        }
    }

    private void recalculatePopUpIndex(int index)
    {
        int indexRemove = -1;

       for (int i = 0; i < indexPopUp.Count; i++)
        {
            if(indexPopUp[i] == index)
            {
                indexRemove = i;
            }

            if(indexPopUp[i] > index)
            {
              indexPopUp[i] -= 1;  
            }
        } 

        if(indexRemove != -1)
        {        
        indexPopUp.RemoveAt(indexRemove);
        }
    }

}

[System.Serializable]
public class ItensInv
{

public int keyIndex;
public Sprite image;
public string nameItem;
public string description;
public bool isItemDestroyable;
public int uses = 1;
public Color corTextoPopUp;

}
