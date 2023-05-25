using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    private int currentPage;

    public GameObject setButton1;
    public GameObject setButton2;
    
    public void OnClick () //Essa funcao vai ficar no evento OnClick do inspector do botao que e o diario
    {
        currentPage = 0;
		foreach (var page in pages ) 
        {
			page.SetActive(false);
		}

        Debug.Log("está funcionando");
		pages[currentPage].SetActive(true);
        setButton1.SetActive(true);
        setButton2.SetActive(true);
    }

	private void SetPage(int page) 
	{
		pages[currentPage].SetActive(false);
		currentPage = page;
		pages[currentPage].SetActive(true);
	}

	public void GotoNextPage()
    {
		SetPage(currentPage + 1);
    }
	public void GotoPreviousPage()
    {
		SetPage(currentPage - 1);
    }
}
