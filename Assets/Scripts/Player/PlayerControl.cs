using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private bool canPass = true;
    private GameObject dialogueBox;
    private DiaryManager diaryManager;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private PlayerInput input;
    private bool facingRight = true;
    private bool canMove = true;
    private bool isEPressed;
    [HideInInspector]
    public bool analisando = false;
    [SerializeField]
    private PlayerPosition playerPositionOnLoad;

    void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("InventoryCanvas").transform.GetChild(0).GetComponent<Inventory>();
        dialogueBox = GameObject.Find("DialogueCanvas").transform.GetChild(0).gameObject;
        diaryManager = FindObjectOfType<DiaryManager>();
        if (gameObject.name == "Player")
        {
            transform.position = playerPositionOnLoad.playerPosition;
        }

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
        if (gameObject.name == "Player")
        {
            input.Player.Enable();
            input.Hand.Disable();
        }
        PassNextSentence();

        if (analisando || inventory.isActiveAndEnabled || (dialogueBox.activeInHierarchy && gameObject.name == "Player") || diaryManager.wasClicked)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
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
        // só movimenta se não estiver no inventário, com o diário aberto ou com diálogo passando
        if (canMove)
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
        if (other.gameObject.CompareTag("Parede"))
        {
            NoMovement();
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Acusado"))
        {
            isEPressed = input.Player.Interact.ReadValue<float>() > 0.1f;
            if (isEPressed)
            {
                Debug.Log("acusado");
            }
        }
    }

    // checa se o jogador tá encostando em um objeto interagivel e se ele vai apertar a tecla de interação
    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Interagivel") || other.gameObject.CompareTag("NPC"))
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

    // passa pra próxima frase com o "E" quando tem diálogo rolando
    private void PassNextSentence()
    {
        if (dialogueBox.activeInHierarchy)
        {
            isEPressed = input.Player.Interact.ReadValue<float>() > 0.1f;
            if (isEPressed && canPass)
            {
                dialogueBox.GetComponent<NextSentence>().Interact();
                canPass = false;
            }
            if (!isEPressed)
            {
                canPass = true;
            }
        }
    }

    // faz um misto quente (abre o diário)
    public void OpenDiary(InputAction.CallbackContext context)
    {
        if (context.performed && !inventory.isActiveAndEnabled && !dialogueBox.activeInHierarchy)
        {
            diaryManager.ActiveDiary();
        }
    }

    // abre o inventário
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

    // sai da cena de análise
    public void Exit(InputAction.CallbackContext context)
    {
        if(SceneManager.GetActiveScene().name != "Acusar")
        {
            var papai = transform.parent.gameObject;
            papai.SetActive(false);
            analisando = false;
        }
    }

    // faz uma coxinha (abre o jogo)
    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pausou");
        }
    }

    // troca o mapa de ações do jogador entre a mão e o personagem
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
