using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    private PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Tab))
    //     {
    //         if (inventory.isActiveAndEnabled)
    //         {
    //             inventory.Hide();
    //         }
    //         else
    //         {
    //             inventory.Show();
    //         }
    //     }
    // }

    public void Inventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inventory.isActiveAndEnabled)
            {
                inventory.Hide();
            }
            else
            {
                inventory.Show();
            }
        }
    }
}
