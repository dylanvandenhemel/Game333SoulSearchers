using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class MapWindow : EditorWindow
{
    private bool bGenerate;
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
        bGenerate = EditorGUILayout.Toggle("GenerateMap", bGenerate);
        //mapCounter = EditorGUILayout.IntField("mapInteger", mapCounter);
        //mapFlaot = EditorGUILayout.FloatField("mapFloat", mapFlaot);
        //mapWindowText = EditorGUILayout.TextField("mapText", mapWindowText);
        mapTiles = EditorGUILayout.ObjectField("mapTransform", mapTiles, typeof(Transform), true)as Transform;
    }

    private void Update()
    {
        if(bGenerate)
        {
            mapTiles.GetComponent<MapGenerator>().mGenerate = true;
            bGenerate = false;
        }
    }




}
