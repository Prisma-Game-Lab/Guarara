using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
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

[CustomEditor(typeof(ItemInteractable))]
public class FuncoesManagerEditor : Editor
{
    private void CreateField(Rect rect, SerializedProperty property, Type type)
    {
        if (type == typeof(int))
        {
            int value;
            if (int.TryParse(property.stringValue, out value))
                value = EditorGUI.IntField(rect, value);
            property.stringValue = value.ToString();
        }
        else if (type == typeof(float))
        {
            float value;
            if (float.TryParse(property.stringValue, out value))
                value = EditorGUI.FloatField(rect, value);
            property.stringValue = value.ToString();
        }
        else if (type == typeof(string))
        {
            property.stringValue = EditorGUI.TextField(rect, property.stringValue);
        }
        else if (type == typeof(bool))
        {
            bool value;
            if (bool.TryParse(property.stringValue, out value))
                value = EditorGUI.Toggle(rect, value);
            property.stringValue = value.ToString();
        }
        else
        {
            EditorGUI.HelpBox(rect, "Tipo de argumento não suportado: " + type.Name, MessageType.Error);
        }
    }

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
            SerializedProperty scriptObjectProperty = elemento.FindPropertyRelative("scriptObject");

            float lineHeight = EditorGUIUtility.singleLineHeight;
            float spacing = 2f;
            rect.y += spacing;
            rect.height = lineHeight;

            float labelWidth = 150; // Aumentado de 100 para 150
            float fieldWidth = rect.width - labelWidth;

            EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, lineHeight), "GameObject");
            EditorGUI.PropertyField(new Rect(rect.x + labelWidth, rect.y, fieldWidth, lineHeight), scriptObjectProperty, GUIContent.none);

            GameObject scriptObject = scriptObjectProperty.objectReferenceValue as GameObject;

            if (scriptObject != null)
            {
                MonoBehaviour[] scripts = scriptObject.GetComponents<MonoBehaviour>();
                List<string> options = new List<string>();
                foreach (MonoBehaviour script in scripts)
                {
                    options.Add(script.GetType().Name);
                }

                SerializedProperty scriptNameProperty = elemento.FindPropertyRelative("scriptName");
                int previousSelectedIndex = options.IndexOf(scriptNameProperty.stringValue);

                rect.y += lineHeight + spacing;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, lineHeight), "Script");
                int selectedIndex = EditorGUI.Popup(new Rect(rect.x + labelWidth, rect.y, fieldWidth, lineHeight), previousSelectedIndex, options.ToArray());
                if (selectedIndex >= 0)
                {
                    scriptNameProperty.stringValue = options[selectedIndex];
                }

                if (scriptNameProperty.stringValue != null)
                {
                    Type scriptType = Type.GetType(scriptNameProperty.stringValue + ", Assembly-CSharp");
                    if (scriptType != null)
                    {
                        MethodInfo[] methods = scriptType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

                        List<string> methodNames = new List<string>();

                        foreach (MethodInfo method in methods)
                        {
                            if (method.DeclaringType == scriptType)
                            {
                                methodNames.Add(method.Name);
                            }
                        }

                        SerializedProperty functionNameProperty = elemento.FindPropertyRelative("functionName");
                        int previousMethodIndex = methodNames.IndexOf(functionNameProperty.stringValue);

                        rect.y += lineHeight + spacing;
                        EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, lineHeight), "Nome da Função");
                        int methodIndex = EditorGUI.Popup(new Rect(rect.x + labelWidth, rect.y, fieldWidth, lineHeight), previousMethodIndex, methodNames.ToArray());

                        SerializedProperty functionArguments = elemento.FindPropertyRelative("functionArguments");

                        if (methodIndex >= 0)
                        {
                            functionNameProperty.stringValue = methodNames[methodIndex];
                
                            MethodInfo method = methods[methodIndex];

                            ParameterInfo[] parameters = method.GetParameters();

                            while (functionArguments.arraySize < parameters.Length)
                            {
                                functionArguments.InsertArrayElementAtIndex(functionArguments.arraySize);
                            }
                            while (functionArguments.arraySize > parameters.Length)
                            {
                                functionArguments.DeleteArrayElementAtIndex(functionArguments.arraySize - 1);
                            }

                            for (int i = 0; i < parameters.Length; i++)
                            {
                                ParameterInfo parameter = parameters[i];
                                SerializedProperty argument = functionArguments.GetArrayElementAtIndex(i);

                                SerializedProperty argumentType = argument.FindPropertyRelative("argumentType");
                                argumentType.stringValue = parameter.ParameterType.Name;

                                SerializedProperty argumentValue = argument.FindPropertyRelative("argumentValue");

                                rect.y += lineHeight + spacing;
                                EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, lineHeight), parameter.Name);
                                CreateField(new Rect(rect.x + labelWidth, rect.y, fieldWidth, lineHeight), argumentValue, parameter.ParameterType);
                            }
                        }
                    }
                }
            }
        };

        funcoesSelecionaveisList.elementHeightCallback = index =>
        {
            SerializedProperty elemento = funcoesSelecionaveisProperty.GetArrayElementAtIndex(index);
            SerializedProperty scriptObjectProperty = elemento.FindPropertyRelative("scriptObject");
            SerializedProperty scriptNameProperty = elemento.FindPropertyRelative("scriptName");
            SerializedProperty functionNameProperty = elemento.FindPropertyRelative("functionName");
            SerializedProperty functionArguments = elemento.FindPropertyRelative("functionArguments");

            int numberOfLines = 3 + functionArguments.arraySize;
        
            return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * numberOfLines;
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

