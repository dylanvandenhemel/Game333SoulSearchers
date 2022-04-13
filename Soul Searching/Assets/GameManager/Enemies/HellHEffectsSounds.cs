using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellHEffectsSounds : MonoBehaviour
{
    public AudioSource growlSound;
    public GameObject followEffect;
    public GameObject angerEffect;

    public void DogGrowl()
    {
        growlSound.volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
        growlSound.Play();
    }

    public void DogEffectOn()
    {
        if (GetComponent<HellHound>().player.GetComponent<Player>().bDogHere)
        {
            followEffect.gameObject.SetActive(true);
            angerEffect.gameObject.SetActive(false);
        }
        else if (GetComponent<HellHound>().player.GetComponent<Player>().bpossessSkel)
        {
            followEffect.gameObject.SetActive(false);
            angerEffect.gameObject.SetActive(true);
        }
    }
    public void DogEffectOff()
    {
        followEffect.gameObject.SetActive(false);
        angerEffect.gameObject.SetActive(false);
    }
}
