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
    private int currentPageRight = 0;
    private int currentPageLeft = 0;

    public GameObject setButton1;
    public GameObject setButton2;
    private bool wasClicked = false;
    
    public void ActiveDiary () //Essa funcao vai ficar no evento OnClick do inspector do botao que e o diario
    {
        if (wasClicked==false)              //O diário é ativado e desativado no mesmo botão
        {
		    foreach (var pageRight in pagesRight ) 
            {
			    pageRight.SetActive(false);
		    }

            pagesRight[currentPageRight].SetActive(true);
            pagesLeft[currentPageLeft].SetActive(true);
            setButton1.SetActive(true);
            setButton2.SetActive(true);
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

    /* public void ActiveLeftPages ()
    {
         currentPageLeft = 0;
		foreach (var pageLeft in pagesLeft ) 
        {
            pageLeft.SetActive(false);
		}

        pagesLeft[currentPageLeft].SetActive(true);
        setButton2.SetActive(true);
    } */
	public void GotoNextPage()
    {   //Preciso de um "for" que limite o acréscimo de páginas
        currentPageRight++;
        currentPageLeft++;
        if(currentPageRight<=pagesRight.Length && currentPageLeft<=pagesLeft.Length)
        {
            SetPageRight(currentPageRight);
            SetPageLeft(currentPageLeft);
            Debug.Log("passou");
        }
		
    }
	public void GotoPreviousPage()
    {
        currentPageRight--;
        currentPageLeft--;
        if(currentPageRight>=0 && currentPageLeft>=0)
        {
            SetPageRight(currentPageRight);
            SetPageLeft(currentPageLeft);
            Debug.Log("voltou");
        }
		
		
    }
}
