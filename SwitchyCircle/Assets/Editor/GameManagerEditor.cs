using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("General Settings:");
        EditorGUI.indentLevel++;
        DrawGeneralSettings();
        EditorGUI.indentLevel--;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Settings:");
        EditorGUI.indentLevel++;
        DrawPlayerSettings();
        EditorGUI.indentLevel--;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Debug Settings:");
        EditorGUI.indentLevel++;
        DrawDebugSettings();
        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawGeneralSettings() {

        EditorGUILayout.PropertyField(serializedObject.FindProperty("menuUI"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("shopUI"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("settingsUI"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gamePlayUI"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameOverUI"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dailyGiftUI"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("shakeController"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("colors"), true);

    }

    private void DrawPlayerSettings()
    {
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("handPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("circlePrefab"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("handSkins"), true);

    }

    private void DrawDebugSettings()
    {

        GameManager gameManager = (GameManager)target;

        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Clear Player Data"))
        {
            
            gameManager.ResetData();
            Debug.Log("Cleared!");

        }

        if (GUILayout.Button("Add gems"))
        {

            gameManager.gems += 250;
            gameManager.SaveData();
            Debug.Log("Unlocked!");

        }

        GUILayout.EndHorizontal();

    }

}
