using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

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
                    MethodInfo methodInfo = funcaoSelecionavel.script.GetType().GetMethod(funcaoSelecionavel.nomeDaFuncao, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(funcaoSelecionavel.script, null);
                    }
                    else
                    {
                        Debug.LogError("Método " + funcaoSelecionavel.nomeDaFuncao + " não encontrado no script " + funcaoSelecionavel.script);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Erro ao invocar o método " + funcaoSelecionavel.nomeDaFuncao + ": " + e.Message);
                }
            }
        }

    }
}

[Serializable]
public class FuncaoSelecionavel
{
    public MonoBehaviour script;
    public string nomeDaFuncao;
}

[CustomEditor(typeof(ItemInteractable))]
public class FuncoesManagerEditor : Editor
{
    private ReorderableList funcoesSelecionaveisList;

    private void OnEnable()
    {
        SerializedProperty funcoesSelecionaveisProperty = serializedObject.FindProperty("funcoesSelecionaveis");

        funcoesSelecionaveisList = new ReorderableList(serializedObject, funcoesSelecionaveisProperty, true, true, true, true);

        funcoesSelecionaveisList.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(rect, "Funções Selecionáveis");
        };

        funcoesSelecionaveisList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty elemento = funcoesSelecionaveisProperty.GetArrayElementAtIndex(index);
            SerializedProperty scriptProperty = elemento.FindPropertyRelative("script");
            SerializedProperty nomeDaFuncaoProperty = elemento.FindPropertyRelative("nomeDaFuncao");

            float lineHeight = EditorGUIUtility.singleLineHeight;
            float spacing = 2f;
            rect.y += spacing;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width - 80, lineHeight), scriptProperty, GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + rect.width - 75, rect.y, 70, lineHeight), nomeDaFuncaoProperty, GUIContent.none);
        };

        funcoesSelecionaveisList.elementHeightCallback = index =>
        {
            SerializedProperty elemento = funcoesSelecionaveisProperty.GetArrayElementAtIndex(index);
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2;
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemInteractablePrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("yPosAdd"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("keyIndex"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("changeSprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("destroyCollider"));
        EditorGUILayout.Space();

        funcoesSelecionaveisList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}
