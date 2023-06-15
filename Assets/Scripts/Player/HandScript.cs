using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandScript : MonoBehaviour
{
    // variáveis

    [SerializeField]
    private float speed;
    [SerializeField]
    private string previousSceneName;
    private Rigidbody2D rb;
    private Vector2 movement;
    private ScenesManager sm;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sm = FindObjectOfType<ScenesManager>();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    // função de movimentar a mão
    public void Movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    // função de interagir com um objeto da cena
    public void Interact(InputAction.CallbackContext context)
    {

    }

    // função de retornar para a cena anterior
    public void Exit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sm.GoToScene(previousSceneName);
        }
    }
}
