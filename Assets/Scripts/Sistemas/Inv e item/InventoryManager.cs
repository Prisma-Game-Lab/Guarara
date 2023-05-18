using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<ItensInv> ItensInv = new List<ItensInv>();

    public bool isInvActive = false;

    [SerializeField]
    private GameObject inventarioPrefab;
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
    private Color corSelecao;

    private bool isHandHoldingItem = false;
    private int indexHolding;

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

    private void CreateInventory()
    {
        Vector3 pos = new Vector3(0f, 0f, 0f);
        inventarioObject = Instantiate(inventarioPrefab, transform.position + pos, Quaternion.identity);
        inventarioObject.GetComponent<RectTransform>().anchoredPosition = pos;
        inventarioObject.transform.SetParent(objPai.transform, false);
        InventoryScript inventarioObjectScript = inventarioObject.GetComponent<InventoryScript>();
        inventarioObjectScript.itemInvPrefab = itemInvPrefab;
        inventarioObjectScript.inventoryManager = this;
        inventarioObjectScript.corFundoCaixa = corFundoCaixa;
        inventarioObjectScript.corCimaCaixa = corCimaCaixa;
        inventarioObjectScript.corSelecao = corSelecao;
    }

    public void ChangeHolding(int index)
    {
        if(index != indexHolding)
        {
            handImageObj.SetActive(true);
            handImage.sprite = ItensInv[index].image;
            indexHolding = index;
            isHandHoldingItem = true;
        }
        else
        {
            handImageObj.SetActive(false);
            isHandHoldingItem = false;
        }
    }

}

[System.Serializable]
public class ItensInv
{

public Sprite image;
public string nameItem;
public string description;
public int keyIndex;

}
