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
    private bool bPlatformActive = false;


    //Detects Object to Trigger
    private void Start()
    {
        if (transform.CompareTag("Trap"))
        {
            bTrap = true;
            Trigger();
        }
        else if (transform.CompareTag("Door"))
        {
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
            Debug.Log("Trap Triggered");
            bTrapActive = true;
        }
        else if(bTrapActive)
        {
            Debug.Log("Trap Stopped");
            bTrapActive = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Physical") && bTrap)
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<Player>().KillSkeleton();
            }
            ActiveTrap();
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
            Debug.Log("Door Triggered");
            gameObject.SetActive(false);
            bDoorActive = true;
        }
        else if(bDoorActive)
        {
            Debug.Log("Door Stop");
            gameObject.SetActive(true);
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
