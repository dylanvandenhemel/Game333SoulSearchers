using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static float masterVolumeSet;
    public static float musicVolumeSet;
    public static float sFXVolumeSet;


    //just for level music volume in camera
    public void Update()
    {
        GetComponent<AudioSource>().volume = masterVolumeSet * musicVolumeSet;
    }
}
