using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // variáveis públicas
    public float speed = 5f;

    // variáveis privadas
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    [SerializeField]
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("Sistemas/Inventory Manager").GetComponent<InventoryManager>();   

        rb = GetComponent<Rigidbody2D>();
        if(GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
    }
    void Update()
    {
        if(inventoryManager.isInvActive == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if(animator != null)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.magnitude);
            }
        }
        else
        {
            movement.x = 0f;
            movement.y = 0f;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
