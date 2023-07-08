using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsNearEvent : MonoBehaviour
{
   public UnityEvent NearNPC;
   
   public void OnCollisionEnter2D (Collision2D collision)
    {
        NearNPC?.Invoke();
    }
    public void OnCollisionExit2D (Collision2D collision)
    {
        NearNPC?.Invoke();
    }
}
