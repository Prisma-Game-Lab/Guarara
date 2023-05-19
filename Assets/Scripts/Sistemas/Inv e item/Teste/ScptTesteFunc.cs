using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScptTesteFunc : MonoBehaviour
{
    [SerializeField]
    GameObject TextoPrefab; 
    [SerializeField]
    GameObject CanvasParent;

    public void FuncaoTestePorta1()
    {
        GameObject texto = Instantiate(TextoPrefab);
        texto.transform.SetParent(CanvasParent.transform, false);
        RectTransform rectTransform = texto.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, -350);
        texto.GetComponent<TextMeshProUGUI>().text = "A porta 1 foi aberta e essa função ligada a ela foi ativa!";
    }

    public void FuncaoTestePorta2()
    {
        GameObject texto = Instantiate(TextoPrefab);
        texto.transform.SetParent(CanvasParent.transform, false);
        RectTransform rectTransform = texto.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);
        texto.GetComponent<TextMeshProUGUI>().text = "A porta 2 foi aberta e essa função ligada a ela foi ativa!";
    }

    public void FuncaoTestePorta3()
    {
        GameObject texto = Instantiate(TextoPrefab);
        texto   .transform.SetParent(CanvasParent.transform, false);
        RectTransform rectTransform = texto.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 320);
        texto.GetComponent<TextMeshProUGUI>().text = "A porta 3 foi aberta e essa função ligada a ela foi ativa!";
    }
}
