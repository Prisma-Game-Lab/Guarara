using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DiaryManager : MonoBehaviour
{
    public UnityEvent QPressed;
    private InputAction interactionAction;
    public InputActionAsset action2;
    [SerializeField] public GameObject[] pagesRight;
    [SerializeField] public GameObject[] pagesLeft;
    private int currentPageRight;
    private int currentPageLeft;

    public GameObject setButton1;
    public GameObject setButton2;
    private bool wasClicked = false;
    
    public void ActiveDiary () //Essa funcao vai ficar no evento OnClick do inspector do botao que e o diario
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
    public void OnQPressed (InputAction.CallbackContext context)
    {
        QPressed?.Invoke();
    }

    void Awake ()
    {
        action2.FindActionMap("Player").FindAction("Interact").performed += OnQPressed;
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

        if (currentPageRight > pagesRight.Length)
        {
            currentPageRight = 0;
        }
        else 
        {
            currentPageRight+=1;
        }
        SetPageRight(currentPageRight);
        SetPageLeft(currentPageRight);

        // for(int i = currentPageRight; i < pagesRight.Length;i++)
        // {

        // }
		
    }
	public void GotoPreviousPage()
    {
        // for(int i = currentPageLeft; i>0 ;i--)
        // {
        //     SetPageRight(i);
        //     SetPageLeft(i);
        // }
        //Preciso de um "for" que limite o acréscimo de páginas
		
        if (currentPageLeft < pagesLeft.Length)
        {
            currentPageLeft  = 0;
        }
        else 
        {
            currentPageLeft-=1;
        }
        SetPageRight(currentPageLeft);
        SetPageLeft(currentPageLeft);

    }
}
