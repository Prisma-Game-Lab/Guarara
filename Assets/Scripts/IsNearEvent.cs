using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsNearEvent : MonoBehaviour
{
   public UnityEvent NearNPC;
   public bool wasCollided = false;
   public string characterTag;
   
   public void OnCollisionEnter2D (Collision2D collision)
    {
        NearNPC?.Invoke();
        wasCollided = true;
        characterTag = gameObject.tag;  
    }
    public void OnCollisionExit2D (Collision2D collision)
    {
        NearNPC?.Invoke();
        wasCollided = false;
    }
}
