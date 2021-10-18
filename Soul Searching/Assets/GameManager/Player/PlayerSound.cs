using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    //whistling
    public AudioSource wistle1;
    public AudioSource wistle2;
    public AudioSource wistle3;
    public AudioSource wistle4;
    public AudioSource wistle5;

    public AudioSource pickUpBones;
    public AudioSource dropBones;

    public void PlayerWistle()
    {
        StartCoroutine(WhistleCoolDown(Random.Range(1, 6)));
    }

    IEnumerator WhistleCoolDown(int soundVal)
    {
        if (soundVal == 1)
        {
            wistle1.Play();
        }
        else if (soundVal == 2)
        {
            wistle2.Play();
        }
        else if (soundVal == 3)
        {
            wistle3.Play();
        }
        else if (soundVal == 4)
        {
            wistle4.Play();
        }
        else if (soundVal == 5)
        {
            wistle5.Play();
        }

        yield return new WaitForSeconds(1f);
    }

    public void PossessBonesSound()
    {
        pickUpBones.Play();
    }

    public void DropBonesSound()
    {
        dropBones.Play();
    }
}
