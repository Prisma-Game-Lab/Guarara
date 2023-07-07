using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public void Interact();
}

public class PlayerControl : MonoBehaviour
{

    // variáveis 
    [SerializeField]
    private float speed = 5f;
    private Inventory inventory;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private PlayerInput input;
    private bool facingRight = true;
    private bool isEPressed;
    [HideInInspector]
    public bool analisando = false;

    void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(0).GetComponent<Inventory>();

        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }
    private void Update()
    {
        if (this.gameObject.name == "Player")
        {
            input.Player.Enable();
            input.Hand.Disable();
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // função de movimento do jogador
    public void Movement(InputAction.CallbackContext context)
    {
        // só movimenta se não estiver no inventário
        if (!analisando && !inventory.isActiveAndEnabled)
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
            if (isEPressed)
            {
                if (other.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }

    public void Inventory(InputAction.CallbackContext context)
    {
        if (context.performed && !analisando)
        {
            if (inventory.isActiveAndEnabled)
            {
                inventory.Hide();
            }
            else
            {
                inventory.Show();
                inventory.ListItems();
            }
        }
    }

    public void Exit(InputAction.CallbackContext context)
    {
        var papai = this.transform.parent.gameObject;
        papai.SetActive(false);
        analisando = false;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pausou");
        }
    }

    public void SwitchActions()
    {
        if (input.Player.enabled)
        {
            input.Player.Disable();
            input.Hand.Enable();
        }
        else
        {
            input.Player.Enable();
            input.Hand.Disable();
        }
    }
}
