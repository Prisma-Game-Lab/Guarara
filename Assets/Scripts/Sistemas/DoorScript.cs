using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // variáveis
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private Vector3 nextPlayerPosition;
    [SerializeField]
    private PlayerPosition playerPosition;
    private ScenesManager sceneLoader;
    //Para portas trancadas
    [Space(15)]
    public bool locked;
    public Item key;
    public InventoryItems inventory;

    void Start()
    {
        sceneLoader = FindObjectOfType<ScenesManager>();
    }

    // troca de cena quando o jogador passa por uma porta
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if(!locked || inventory.list.Contains(key))
            {    
                sceneLoader.GoToScene(nextSceneName);
                playerPosition.playerPosition = nextPlayerPosition;
            }
        }
    }
}
