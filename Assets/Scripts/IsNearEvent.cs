using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsNearEvent : MonoBehaviour
{
    public UnityEvent NearNPC;
    public bool wasCollided = false;
    public string characterTag;
    [SerializeField]
    private ItemScript scriptItem;

    private void Awake()
    {
        scriptItem = this.gameObject.GetComponent<ItemScript>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            NearNPC?.Invoke();
            wasCollided = true;
        }
        characterTag = collision.gameObject.tag;
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        NearNPC?.Invoke();
        wasCollided = false;
    }
}
