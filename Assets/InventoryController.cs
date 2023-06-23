using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
