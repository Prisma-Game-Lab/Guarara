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
            
        }
        else
        {
            PopUp.SetActive(true);
            
        }
        
        
        
    }
}
