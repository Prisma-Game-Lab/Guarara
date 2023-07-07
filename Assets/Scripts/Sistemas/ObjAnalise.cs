using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjAnalise : MonoBehaviour
{
    // variáveis
    [SerializeField]
    private GameObject maozinha;

    private void Awake()
    {
        Instantiate(maozinha, transform.position, Quaternion.identity);
    }

}
