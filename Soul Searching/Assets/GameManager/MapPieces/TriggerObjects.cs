using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PUT ON ALL DOOR TRAPS ETC.
public class TriggerObjects : MonoBehaviour
{
    private bool bTrap = false;
    private bool bDoor = false;
    private bool bPlatform = false;

    //On off system
    public bool bTrapActive = false;
    public bool bDoorActive = false;
    private bool bDoorStart;
    private bool bPlatformActive = false;

    private AudioSource trapSound;
    //Detects Object to Trigger
    private void Start()
    {
        if (transform.CompareTag("Trap"))
        {
            bTrap = true;
            trapSound = GetComponentInChildren<AudioSource>();
            Trigger();
        }
        else if (transform.CompareTag("Door"))
        {
            bDoorStart = bDoorActive;

            bDoor = true;
            Trigger();
        }
        else if (transform.CompareTag("Platform"))
        {
            bPlatform = true;
            Trigger();
        }
    }

    public void Trigger()
    {
        if(bTrap)
        {
            ActiveTrap();
        }
        else if(bDoor)
        {
            ActiveDoor();
        }
        else if(bPlatform)
        {
            ActivePlatform();
        }
    }


    //Trap Only
    private void ActiveTrap()
    {
        if(!bTrapActive)
        {
            //Debug.Log("Trap Triggered");
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            bTrapActive = true;
        }
        else if(bTrapActive)
        {
            //Debug.Log("Trap Stopped");
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            bTrapActive = false;
        }
    }

    IEnumerator TrapTimer()
    {
        yield return new WaitForSeconds(1);
        ActiveTrap();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Physical") && bTrap)
        {
            if(other.CompareTag("Zombie"))
            {
                Debug.Log("Zombie");
                other.GetComponentInParent<Zombie>().KillEnemy();
            }
            if(other.CompareTag("Player"))
            {
                other.GetComponent<Player>().KillSkeleton();
            }
            trapSound.Play();
            ActiveTrap();
            StartCoroutine(TrapTimer());
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Physical") && bTrap)
        {
            ActiveTrap();
        }
    }

    //Door Only
    private void ActiveDoor()
    {
        if(!bDoorActive)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("doorOpen");
            //gameObject.SetActive(false);
            bDoorActive = true;
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("doorClose");
            //gameObject.SetActive(true);
            bDoorActive = false;
        }
    }


    //Platform Only
    private void ActivePlatform()
    {
        if(!bPlatformActive)
        {
            transform.GetComponent<MovingPlatform>().MovePlatform();
            bPlatformActive = true;
        }
        else if (bPlatformActive)
        {
            transform.GetComponent<MovingPlatform>().ReturnPlatform();
            bPlatformActive = false;
        }
    }
}
