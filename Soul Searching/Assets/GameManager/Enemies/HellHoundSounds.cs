using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellHoundSounds : MonoBehaviour
{
    public AudioSource growlSound;

    public void DogGrowl()
    {
        growlSound.volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
        growlSound.Play();
    }
}
