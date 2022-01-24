using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public static float masterVolumeSet;
    public static float musicVolumeSet;
    public static float sFXVolumeSet;

    //for locked levels
    public static int levelsUnlocked;

    public void Start()
    {
        if (levelsUnlocked < SceneManager.GetActiveScene().buildIndex)
        {
            levelsUnlocked = SceneManager.GetActiveScene().buildIndex;
        }
    }

    //just for level music volume in camera
    public void Update()
    {
        GetComponent<AudioSource>().volume = masterVolumeSet * musicVolumeSet;

        musicVolumeSet *= masterVolumeSet;
        sFXVolumeSet *= masterVolumeSet;
    }
}
