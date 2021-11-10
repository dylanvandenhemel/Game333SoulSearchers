using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    private int mapChildCount;
    private int counter;
    public List<GameObject> origTile;

    public void Generate()
    {
        //fills array with current tiles
        mapChildCount = transform.childCount;
        origTile.Clear();
        for (counter = 0; counter < mapChildCount; counter++)
        {
            origTile.Add(transform.GetChild(counter).gameObject);
        }

        //Activates each tiles map piece
        for (counter = 0; counter < mapChildCount; counter++)
        {
            if (transform.GetChild(counter).GetComponent<Map>() != null)
            {
                transform.GetChild(counter).GetComponent<Map>().AutoMap();
            }
        }

        //Deletes old tiles
        for (counter = 0; counter < origTile.Count; counter++)
        {
            if (transform.GetChild(counter).GetComponent<Map>() != null)
            {
                DestroyImmediate(origTile[counter]);
            }

        }
    }

    public void Clear()
    {
        for (counter = 0; counter < origTile.Count; counter++)
        {
            if(transform.childCount > 0)
            {
                Undo.DestroyObjectImmediate(transform.GetChild(0).gameObject);
            }

        }
    }

    public void AutoCorner()
    {
        for (counter = 0; counter < mapChildCount; counter++)
        {
            transform.GetChild(counter).GetComponent<Map>().AutoPillar();
        }
    }
}
#endif