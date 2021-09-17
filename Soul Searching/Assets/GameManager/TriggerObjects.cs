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
    private bool bDoorActive = false;
    private bool bPlatformActive = false;


    //Detects Object to Trigger
    private void Start()
    {
        if (transform.CompareTag("Trap"))
        {
            bTrap = true;
        }
        else if (transform.CompareTag("Door"))
        {
            bDoor = true;
        }
        else if (transform.CompareTag("Platform"))
        {
            bPlatform = true;
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

    public void StopTrigger()
    {
        if (bTrap)
        {
            StopTrap();
        }
        else if (bDoor)
        {
            StopDoor();
        }
        else if (bPlatform)
        {
            StopPlatform();
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
    }
    private void StopTrap()
    {
        if (bTrapActive)
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
            StopTrap();
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
    }
    private void StopDoor()
    {
        if (bDoorActive)
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
    }
    private void StopPlatform()
    {
        if(bPlatformActive)
        {
            transform.GetComponent<MovingPlatform>().ReturnPlatform();
            bPlatformActive = false;
        }
    }
}
