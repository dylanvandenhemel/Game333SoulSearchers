using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellHoundSounds : MonoBehaviour
{
    private bool bgrowling;

    public AudioSource growlSound;

    public void DogGrowl()
    {
        if(!bgrowling)
        {
            growlSound.Play();
            bgrowling = true;

        }
    }

    IEnumerator growlWait()
    {
        yield return new WaitForSeconds(1);
        bgrowling = false;
    }
}
