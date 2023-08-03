using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    [SerializeField] public GameObject[] pagesRight;
    [SerializeField] public GameObject[] pagesLeft;
    private int currentPageRight;
    private int currentPageLeft;

    public GameObject setButton1;
    public GameObject setButton2;
    private bool wasClicked = false;
    
    public void OnClick () //Essa funcao vai ficar no evento OnClick do inspector do botao que e o diario
    {
        if (wasClicked==false)              //O diário é ativado e desativado no mesmo botão
        {
            currentPageRight = 0;
		    foreach (var pageRight in pagesRight ) 
            {
			    pageRight.SetActive(false);
		    }

            pagesRight[currentPageRight].SetActive(true);
            setButton1.SetActive(true);
            wasClicked = true;
        }
        else if (wasClicked==true)
        {
            pagesRight[currentPageRight].SetActive(false);
            pagesLeft[currentPageLeft].SetActive(false);
            setButton1.SetActive(false);
            setButton2.SetActive(false);
            wasClicked = false;
        }
        
        
    }

	private void SetPageRight(int pageRight) 
	{
		pagesRight[currentPageRight].SetActive(false);
		currentPageRight = pageRight;
		pagesRight[currentPageRight].SetActive(true);
	}

    
	private void SetPageLeft(int pageLeft) 
	{
		pagesLeft[currentPageLeft].SetActive(false);
		currentPageLeft = pageLeft;
		pagesLeft[currentPageLeft].SetActive(true);
	}

    public void ActiveLeftPages ()
    {
         currentPageLeft = 0;
		foreach (var pageLeft in pagesLeft ) 
        {
            pageLeft.SetActive(false);
		}

        pagesLeft[currentPageLeft].SetActive(true);
        setButton2.SetActive(true);
    }
	public void GotoNextPage()
    {   //Preciso de um "for" que limite o acréscimo de páginas

        for(int i = currentPageRight; i < pagesRight.Length;i++)
        {
            SetPageRight(i);
            SetPageLeft(i);
        }
		
    }
	public void GotoPreviousPage()
    {
        for(int i = currentPageLeft; i>0 ;i--)
        {
            SetPageRight(i);
            SetPageLeft(i);
        }
        //Preciso de um "for" que limite o acréscimo de páginas
		
    }
}
