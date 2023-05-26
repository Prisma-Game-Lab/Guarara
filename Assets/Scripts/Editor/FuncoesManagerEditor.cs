using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Reflection;
using System.Linq;


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

