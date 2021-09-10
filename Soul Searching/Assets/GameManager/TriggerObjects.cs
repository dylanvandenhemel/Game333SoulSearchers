using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PUT ON ALL DOOR TRAPS ETC.
public class TriggerObjects : MonoBehaviour
{
    private bool bTrap = false;
    private bool bDoor = false;

    private bool bPlatform = false;

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
        Debug.Log("Trap Triggered");
    }
    private void StopTrap()
    {
        Debug.Log("Trap Stopped");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Physical") && bTrap)
        {
            ActiveTrap();
        }
    }

    //Door Only
    private void ActiveDoor()
    {
        Debug.Log("Door Triggered");
    }
    private void StopDoor()
    {
        Debug.Log("Door Stopped");
    }


    //Platform Only
    private void ActivePlatform()
    {
        transform.GetComponent<MovingPlatform>().MovePlatform();
    }
    private void StopPlatform()
    {
        transform.GetComponent<MovingPlatform>().ReturnPlatform();
    }
}
