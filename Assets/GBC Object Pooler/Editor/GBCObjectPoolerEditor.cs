/*
 * GBC Object Pooler - Unity Editor Script
 * Developed by John Schumacher 2019
 * http:// gamesbycandlelight.com
 * https://github.com/schizoid2k/Unity-GBC-Object-Pool
 * @CandlelightGame (Twitter)
 * 
 * The use of the GBCObjectPooler Unity asset is free.  This is a simple, yet effective,
 * object pooler library for Unity.  I have open-sourced the development of this
 * library, and I encourage you to share your updates with the community.
*/

using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using GBCObjectPooler;

[CustomEditor(typeof(Pooler))]

public class GBCObjectPoolerEditor : Editor
{
    private Pooler gbcObjectPooler;
    private SerializedObject _target;
    private SerializedProperty poolList;
    private int listSize;

    void OnEnable() {
        gbcObjectPooler = (Pooler)target;
        _target = new SerializedObject(gbcObjectPooler);
        poolList = _target.FindProperty("Pools");
    }

    public override void OnInspectorGUI() {
        _target.Update();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        listSize = poolList.arraySize;
        listSize = EditorGUILayout.IntField("Pools", listSize);

        if (listSize != poolList.arraySize) {
            while (listSize > poolList.arraySize) {
                poolList.InsertArrayElementAtIndex(poolList.arraySize);
            }
            while (listSize < poolList.arraySize) {
                poolList.DeleteArrayElementAtIndex(poolList.arraySize - 1);
            }
        }

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();

        // Clicking 'New' button will insert a new pool at the top of the list
        if (GUILayout.Button("Add New Object Pool", GUILayout.Height(25))) {
            gbcObjectPooler.Pools.Insert(0, new Pooler.PoolItem());
        }

        if (GUILayout.Button("Create Constants File", GUILayout.Height(25))) {
            CreateConstants();
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        DrawUILine(Color.gray, 2, 10);
        EditorGUILayout.Space();

        //Display our list to the inspector window

        for (int i = 0; i < poolList.arraySize; i++) {
            SerializedProperty MyListRef = poolList.GetArrayElementAtIndex(i);
            SerializedProperty MyPooledObject = MyListRef.FindPropertyRelative("PooledObject");
            SerializedProperty MyPooledAmount = MyListRef.FindPropertyRelative("PooledAmount");
            SerializedProperty MyPoolName = MyListRef.FindPropertyRelative("PoolName");

            EditorGUILayout.PropertyField(MyPoolName);
            EditorGUILayout.PropertyField(MyPooledObject);
            EditorGUILayout.PropertyField(MyPooledAmount);

            EditorGUILayout.Space();

            //Remove selected pool from the List
            if (GUILayout.Button("Remove This Pool")) {
                poolList.DeleteArrayElementAtIndex(i);
            }

            EditorGUILayout.Space();
            DrawUILine(Color.gray, 2, 10);

            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        //Apply the changes to our list
        _target.ApplyModifiedProperties();
    }

    public static void DrawUILine(Color color, int thickness = 2, int padding = 10) {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, color);
    }

    private void CreateConstants() {
        string currentSceneName = SceneManager.GetActiveScene().name.Replace(" ", String.Empty);
        string headerFileLocation = Directory.GetCurrentDirectory() + "\\Assets\\GBC Object Pooler\\Editor\\Templates\\header.txt";
        string constantsFileName = "GBCPool_" + currentSceneName + "_Constants.cs";
        string generatedFileLocationRelativePath = "Assets\\GBC Object Pooler\\Constants\\";
        string generatedFileLocationFullPath = Directory.GetCurrentDirectory() + "\\" + generatedFileLocationRelativePath;
        string constantsFileNameRelativePath = generatedFileLocationRelativePath + constantsFileName;
        string constantsFileNameFullPath = Directory.GetCurrentDirectory() + "\\" + generatedFileLocationRelativePath + constantsFileName;

        // read the template
        string headerText = File.ReadAllText(headerFileLocation);
        headerText = headerText.Replace("__SCENE__", currentSceneName);

        // check if generated folder exists
        if (!Directory.Exists(generatedFileLocationFullPath)) Directory.CreateDirectory(generatedFileLocationFullPath);
        
        // Remove old file, if exists
        if (File.Exists(constantsFileNameFullPath)) File.Delete(constantsFileNameFullPath);

        // create constants file
        using (StreamWriter sw = File.CreateText(constantsFileNameFullPath)) {
            sw.Write(headerText + "\n\n");
            sw.WriteLine("namespace GBCObjectPooler");
            sw.WriteLine("{");
            sw.WriteLine("\tpublic static class GBCPool_" + currentSceneName + "_Constants");
            sw.WriteLine("\t{");

            for (int i = 0; i < listSize; i++) {
                SerializedProperty MyListRef = poolList.GetArrayElementAtIndex(i);
                string poolName = MyListRef.FindPropertyRelative("PoolName").stringValue;
                //sw.WriteLine("\t\tpublic static string GBCPool_" + currentSceneName + "_" + poolName + "= \"" + poolName + "\";");
                sw.WriteLine("\t\tpublic static string " + poolName + " = \"" + poolName + "\";");
            }

            sw.WriteLine("\t}\n}");
        }

        AssetDatabase.ImportAsset(constantsFileNameRelativePath);
        Debug.Log("Constants File " + constantsFileNameRelativePath + " created");
    }
}
