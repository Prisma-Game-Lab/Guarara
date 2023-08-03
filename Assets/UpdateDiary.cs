using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpdateDiary : MonoBehaviour
{
   public UnityEvent DiaryEvent;
   public GameObject[] NecessaryItems;
   public bool isEqual = false;
   public Folha folha;
   private int indexPageRight = 0;
   private int indexPageLeft = 1;
   private int wayPage = 0;
   public int indexNecessaItems;
   private List<Item> currentItems = new List<Item>();
   
   public void DiaryEventActive ()
   {
      if(isEqual == true || GetComponent<DialogueTrigger>().isFirstTime == true)
      {
         DiaryEvent?.Invoke();
      }   
   }

   public void OnUpdateDiary ()
   {
      if(wayPage==0)
        {
            GetComponent<DiaryManager>().pagesRight[indexPageRight] = folha.folhaDiario;
            wayPage = 1;
            indexPageRight++;
        }
        GetComponent<DiaryManager>().pagesLeft[indexPageLeft] =  folha.folhaDiario;
        wayPage = 0;    
        indexPageLeft++; 
   }

   public void checkIfIsEqual ()
   {
      currentItems = GetComponent<InventoryItems>().list;
      foreach(Item item in currentItems )
      {
         if(item == NecessaryItems[indexNecessaItems])
         {
            indexNecessaItems++;
            isEqual = true;
            
         }
         isEqual = false;
      }
   }
}

