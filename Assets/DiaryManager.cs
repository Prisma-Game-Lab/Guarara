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
    public bool wasClicked = false;


    public void ActiveDiary() //Essa funcao vai ficar no evento OnClick do inspector do botao que e o diario
    {
        if (wasClicked == false)              //O diário é ativado e desativado no mesmo botão
        {
            foreach (var pageRight in pagesRight)
            {
                pageRight.SetActive(false);
            }

            pagesRight[currentPageRight].SetActive(true);
            pagesLeft[currentPageLeft].SetActive(true);
            setButton1.SetActive(true);
            setButton2.SetActive(true);
            wasClicked = true;
            AudioManager.instance.PlaySfx("caderno");
        }
        else if (wasClicked == true)
        {
            pagesRight[currentPageRight].SetActive(false);
            pagesLeft[currentPageLeft].SetActive(false);
            setButton1.SetActive(false);
            setButton2.SetActive(false);
            wasClicked = false;
        }


    }
    /*  public void OnQPressed (InputAction.CallbackContext context)
     {
         QPressed?.Invoke();
     }

     void Awake ()
     {
         action2.FindActionMap("Player").FindAction("Interact").performed += OnQPressed;
     }
      */
    public void GotoNextPage()
    {
        Debug.Log(currentPageLeft);
        Debug.Log(currentPageRight);
        if (currentPageRight < (pagesRight.Length - 1) && currentPageLeft < (pagesLeft.Length - 1))
        {
            pagesRight[currentPageRight].SetActive(false);
            pagesLeft[currentPageLeft].SetActive(false);
            pagesRight[currentPageRight + 1].SetActive(true);
            pagesLeft[currentPageLeft + 1].SetActive(true);
            currentPageLeft++;
            currentPageRight++;
            Debug.Log("passou");
            AudioManager.instance.PlaySfx("cadernoPagina");
        }

    }
    public void GotoPreviousPage()
    {
        if (currentPageRight > 0 && currentPageLeft > 0)
        {
            Debug.Log(currentPageLeft);
            Debug.Log(currentPageRight);
            pagesRight[currentPageRight].SetActive(false);
            pagesLeft[currentPageLeft].SetActive(false);
            currentPageLeft--;
            currentPageRight--;
            Debug.Log(currentPageLeft);
            Debug.Log(currentPageRight);
            pagesRight[currentPageRight].SetActive(true);
            pagesLeft[currentPageLeft].SetActive(true);
            Debug.Log("voltou");
            AudioManager.instance.PlaySfx("cadernoPagina");
        }

    }
}
