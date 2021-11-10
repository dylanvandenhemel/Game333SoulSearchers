using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;

[ExecuteInEditMode]
public class MapWindow : EditorWindow
{
    //private bool bGenerate;
    //private int mapCounter;
    //private float mapFlaot;
    //private string mapWindowText;
    private Transform mapTiles;
    [MenuItem("Map Generator/Generator")]


    public static void ShowWindow()
    {
        GetWindow(typeof(MapWindow));
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Generator"))
        {
            mapTiles.GetComponent<MapGenerator>().Generate();
        }

        if (GUILayout.Button("AutoCorner"))
        {
            mapTiles.GetComponent<MapGenerator>().AutoCorner();
        }

        mapTiles = EditorGUILayout.ObjectField("mapTransform", mapTiles, typeof(Transform), true)as Transform;
        if (GUILayout.Button("Clear"))
        {
            mapTiles.GetComponent<MapGenerator>().Clear();
        }
        //bGenerate = EditorGUILayout.Toggle("GenerateMap", bGenerate);
        //mapCounter = EditorGUILayout.IntField("mapInteger", mapCounter);
        //mapFlaot = EditorGUILayout.FloatField("mapFloat", mapFlaot);
        //mapWindowText = EditorGUILayout.TextField("mapText", mapWindowText);
    }
}
#endif