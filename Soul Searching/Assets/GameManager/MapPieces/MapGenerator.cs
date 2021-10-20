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

            //fills array with current tiles
            mapChildCount = transform.childCount;
            origTile.Clear();
            for(counter = 0; counter < mapChildCount; counter++)
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

        //places pillers in needed corners
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