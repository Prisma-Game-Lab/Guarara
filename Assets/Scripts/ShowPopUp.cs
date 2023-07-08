using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUp : MonoBehaviour
{
    public GameObject PopUp;
    
    public void ShowSignal ()
    {
        if(PopUp.activeInHierarchy == true)
        {
            PopUp.SetActive(false);
            Debug.Log("colidiu!");
        }
        else
        {
            PopUp.SetActive(true);
            Debug.Log("colidiu!");
        }
        
        
        
    }
}
