using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]


public class MapGenerator : MonoBehaviour
{
    public bool mGenerate;

    private int mapChildCount;
    private int counter;
    public List<GameObject> origTile;
    public bool bAutoPillar;
    public void Update()
    {
        if(mGenerate)
        {
            mGenerate = false;
            mapChildCount = transform.childCount;
            origTile.Clear();
            for(counter = 0; counter < mapChildCount; counter++)
            {
                origTile.Add(transform.GetChild(counter).gameObject);
            }

            for (counter = 0; counter < mapChildCount; counter++)
            {
                transform.GetChild(counter).GetComponent<Map>().AutoMap();
            }          

            for (counter = 0; counter < origTile.Count; counter++)
            {
                DestroyImmediate(origTile[counter]);
            }
        }

        if (bAutoPillar)
        {
            for (counter = 0; counter < mapChildCount; counter++)
            {
                transform.GetChild(counter).GetComponent<Map>().AutoPillar();
            }
            bAutoPillar = false;

        }
    }
}
#endif