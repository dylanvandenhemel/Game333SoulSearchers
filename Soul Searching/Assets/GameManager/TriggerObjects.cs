using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PUT ON ALL DOOR TRAPS ETC.
public class TriggerObjects : MonoBehaviour
{
    private bool bTrap = false;

    private void Start()
    {
        if (transform.CompareTag("Trap"))
        {
            bTrap = true;
        }
    }

    //Detects Object to Trigger
    public void Trigger()
    {
        if(transform.CompareTag("Trap"))
        {
            ActiveTrap();
        }
        else if(transform.CompareTag("Door"))
        {
            ActiveDoor();
        }        
    }

    public void StopTrigger()
    {
        if (transform.CompareTag("Trap"))
        {
            StopTrap();
        }
        else if (transform.CompareTag("Door"))
        {
            StopDoor();
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
}
