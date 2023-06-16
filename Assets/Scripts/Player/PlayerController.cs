using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public void Interact();
}

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
    private bool facingRight = true;
    private bool isEPressed;

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
                animator.SetFloat("Speed", movement.magnitude);
                if (movement.x > 0 && !facingRight)
                {
                    Flip();
                }
                else if (movement.x < 0 && facingRight)
                {
                    Flip();
                }
            }
        }
        else
        {
            NoMovement();
        }

    }
    public void NoMovement()
    {
        movement = Vector2.zero;
        animator.SetFloat("Speed", 0);
    }


    // gira o sprite dependendo da direção na qual o jogador está andando (esquerda ou direita)
    private void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    // para o jogador quando ele encontra uma parede
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Parede")
        {
            NoMovement();
        }

    }

    // checa se o jogador tá encostando em um objeto interagivel e se ele vai apertar a tecla de interação
    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Interagivel")
        {
            isEPressed = input.Player.Interact.ReadValue<float>() > 0.1f;
            Debug.Log(isEPressed);
            if (isEPressed)
            {
                other.gameObject.GetComponent<ObjAnalise>().Interact();
            }
        }
    }
}
