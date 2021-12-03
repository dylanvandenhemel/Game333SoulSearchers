using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    public int zombieSoundOFFSET;

    public AudioSource zombieIdle1;
    public AudioSource zombieIdle2;
    public AudioSource zombieIdle3;

    public AudioSource zombieSees;
    public AudioSource zombieDies;

    private bool bSoundWait;
    private bool bSeesWait;

    public void Start()
    {
        zombieIdle1.volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
        zombieIdle2.volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
        zombieIdle3.volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;

        zombieSees.volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
        zombieDies.volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
    }

    void Update()
    {
        if(zombieSoundOFFSET > 3 || zombieSoundOFFSET < -3)
        {
            Debug.Log("OFFSET ment for when multiple zombies, for best consistancy add or subruct seconds to a maximum of 3");
        }

        if(!bSoundWait)
        {
            StartCoroutine(idleSound(Random.Range(1, 4)));
        }
    }

    IEnumerator idleSound(int idleRand)
    {
        bSoundWait = true;
        yield return new WaitForSeconds(7 + zombieSoundOFFSET);

        if (idleRand == 1)
        {
            zombieIdle1.Play();
        }
        else if (idleRand == 2)
        {
            zombieIdle2.Play();
        }
        else if (idleRand == 3)
        {
            zombieIdle3.Play();
        }

        bSoundWait = false;
        bSeesWait = false;
    }

    public void ZombieSeesSound()
    {
        if(!bSeesWait)
        {
            zombieSees.Play();
            bSeesWait = true;
        }
    }

    public void ZombieDiesSound()
    {
        zombieDies.Play();
    }
}
