using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // variáveis públicas
    public float speed = 5f;

    // variáveis privadas
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private PlayerInput input;

    [SerializeField]
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
    void Update()
    {
        if (inventoryManager.isInvActive == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (animator != null)
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
        rb.velocity = movement * speed;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += Movement;
        input.Player.Movement.canceled += NoMovement;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= Movement;
        input.Player.Movement.canceled -= NoMovement;
    }

    private void Movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void NoMovement(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
    }
}
