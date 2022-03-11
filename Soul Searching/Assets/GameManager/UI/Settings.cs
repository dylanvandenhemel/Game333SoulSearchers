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
    public static int levelMenuUnlocked;
    public static int levelsUnlocked;

    public void Start()
    {
        if (levelsUnlocked < SceneManager.GetActiveScene().buildIndex)
        {
            levelsUnlocked = SceneManager.GetActiveScene().buildIndex;
        }

        //after 10 catacombs levels unlocks dungeon levels
        if (levelsUnlocked == 11)
        {
            levelMenuUnlocked++;
        }
    }

    //just for level music volume in camera
    public void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicVolumeSet *= masterVolumeSet;
            sFXVolumeSet *= masterVolumeSet;

            GetComponent<AudioSource>().volume = musicVolumeSet;
        }
    }
}
