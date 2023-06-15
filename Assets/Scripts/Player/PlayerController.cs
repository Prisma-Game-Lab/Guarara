using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // variáveis 
    [SerializeField]
    private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private PlayerInput input;
    private InventoryManager inventoryManager;

    void Awake()
    {
        input = new PlayerInput();
        inventoryManager = FindObjectOfType<InventoryManager>();
        rb = GetComponent<Rigidbody2D>();

        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += Movement;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= Movement;
    }

    // função de movimento do jogador
    public void Movement(InputAction.CallbackContext context)
    {
        // só movimenta se não estiver no inventário
        if (!inventoryManager.isInvActive)
        {
            movement = context.ReadValue<Vector2>();

            // código correspondente a animação
            if (animator != null)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.magnitude);
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {

    }
}
