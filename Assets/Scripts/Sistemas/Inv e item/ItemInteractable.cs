using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;

public class ItemInteractable : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public int keyIndex;
    public Sprite changeSprite;
    public List<GameObject> destroyCollider = new List<GameObject>();
    public List<FuncaoSelecionavel> funcoesSelecionaveis = new List<FuncaoSelecionavel>();

    [SerializeField]
    private GameObject itemInteractablePrefab;
    [SerializeField]
    private float yPosAdd;

    private GameObject itemInteractableObject;
    private bool podeIniciarD;

    private bool wasInteracted = false;

    private void Start() 
    {
        inventoryManager = GameObject.Find("Sistemas/Inventory Manager").GetComponent<InventoryManager>();  
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(wasInteracted == false)
            {
                Vector3 pos = Vector3.up;
                pos.y += yPosAdd;

                if (itemInteractableObject == null)
                {
                    itemInteractableObject = Instantiate(itemInteractablePrefab, transform.position + pos, Quaternion.identity);
                }

                podeIniciarD = true;
                StartCoroutine(WaitingClick());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (itemInteractableObject != null)
            {
                Destroy(itemInteractableObject);
                itemInteractableObject = null;
            }

            podeIniciarD = false;
        }
    }

    private IEnumerator WaitingClick()
    {
        while (podeIniciarD)
        {
            if(inventoryManager.isInvActive == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CheckIfValid();
                }
            }

            yield return null;
        }
    }

    private void CheckIfValid()
    {
        if(inventoryManager.indexHolding != -1)
        {
            if(inventoryManager.ItensInv[inventoryManager.indexHolding].keyIndex == keyIndex)
            {
                ExecuteFunctions();
                inventoryManager.itemUsed(keyIndex);
                wasInteracted = true;
            }
        }
    }

    private void ExecuteFunctions()
    {
        if(changeSprite != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = changeSprite;
        }

        if(destroyCollider.Count > 0)
        {
            for (int i = 0; i < destroyCollider.Count; i++)
            {
                Destroy(destroyCollider[i]);
            }
        }

        if(funcoesSelecionaveis.Count > 0)
        {
            foreach (var funcaoSelecionavel in funcoesSelecionaveis)
            {
                try
                {
                    GameObject scriptObject = funcaoSelecionavel.scriptObject;
                    string scriptName = funcaoSelecionavel.scriptName;
                    string functionName = funcaoSelecionavel.functionName;
                    List<FunctionArgument> functionArguments = funcaoSelecionavel.functionArguments;

                    MonoBehaviour[] scripts = scriptObject.GetComponents<MonoBehaviour>();
                    MonoBehaviour script = scripts.FirstOrDefault(s => s.GetType().Name == scriptName);

                    if (script != null)
                    {
                        MethodInfo methodInfo = script.GetType().GetMethod(functionName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (methodInfo != null)
                        {
                            List<object> args = new List<object>();
                            ParameterInfo[] parameters = methodInfo.GetParameters();

                            if (parameters.Length == functionArguments.Count)
                            {
                                for (int i = 0; i < parameters.Length; i++)
                                {
                                    FunctionArgument argument = functionArguments[i];
                                    Type argumentType = parameters[i].ParameterType;

                                    object value = Convert.ChangeType(argument.argumentValue, argumentType);
                                    args.Add(value);
                                }

                            methodInfo.Invoke(script, args.ToArray());
                            }
                            else
                            {
                                Debug.LogError("Número incorreto de argumentos para o método " + functionName + " no script " + scriptName);
                            }
                        }
                        else
                        {
                            Debug.LogError("Método " + functionName + " não encontrado no script " + scriptName);
                        }
                    }
                    else
                    {
                        Debug.LogError("Script " + scriptName + " não encontrado no GameObject");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Erro ao invocar o método " + funcaoSelecionavel.functionName + ": " + e.Message);
                }
            }
        }

    }
}

[Serializable]
public class FunctionArgument
{
    public string argumentType;
    public string argumentValue;
}

[Serializable]
public class FuncaoSelecionavel
{
    public GameObject scriptObject;
    public string scriptName;
    public string functionName;
    public List<FunctionArgument> functionArguments;
}


