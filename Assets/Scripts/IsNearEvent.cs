using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsNearEvent : MonoBehaviour
{
   public UnityEvent NearNPC;
   public bool wasCollided = false;
   public string characterName;
   
   public void OnCollisionEnter2D (Collision2D collision)
    {
        NearNPC?.Invoke();
        wasCollided = true;
        characterName = collision.gameObject.name;
    }
    public void OnCollisionExit2D (Collision2D collision)
    {
        NearNPC?.Invoke();
        wasCollided = false;
    }
}
