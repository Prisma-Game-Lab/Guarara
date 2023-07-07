using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandScript : MonoBehaviour
{
    // variáveis

    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private GameObject papai;
    private PlayerControl player;
    private PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        papai = this.transform.parent.gameObject;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }

    // função de movimentar a mão
    public void Movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        Debug.Log("Movimentou");
    }

    // função de interagir com um objeto da cena
    public void Interact(InputAction.CallbackContext context)
    {

    }

    // função de fechar a análise
    public void Exit(InputAction.CallbackContext context)
    {
        papai.SetActive(false);
        player.analisando = false;
        Debug.Log("saiu");
    }
}
