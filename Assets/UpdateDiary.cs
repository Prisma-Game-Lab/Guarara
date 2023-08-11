using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpdateDiary : MonoBehaviour
{
   public UnityEvent DiaryEvent;
   public Item[] NecessaryItems;
   public bool isEqual = false;
   public Folha folha;
   private int indexPageRight = 0;
   private int indexPageLeft = 1;
   private int wayPage = 0;
   public int indexNecessaItems;
   public InventoryItems currentItems;
   
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
      foreach (Item item in NecessaryItems)
      {
         if(currentItems.list.Contains(item))
         {
            isEqual = true;
            if(indexNecessaItems <= (NecessaryItems.Length - 1))
            {
               indexNecessaItems++;
            }
         
            return;
         }
         
      }
      isEqual = false;

   }
   void Update ()
   {
      checkIfIsEqual();
   }
}

