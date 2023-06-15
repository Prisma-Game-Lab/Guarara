using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    private ScenesManager sceneLoader;
    void Start()
    {
        sceneLoader = GameObject.FindWithTag("SceneManager").GetComponent<ScenesManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            Debug.Log("Transicionou de cena");
        }
    }
}
