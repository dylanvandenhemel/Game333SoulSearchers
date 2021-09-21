using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    public bool mGenerate;
    public static Action Generate = delegate { };


    public void Update()
    {
        if(mGenerate)
        {
            Generate();
            mGenerate = false;
        }
    }

}
