using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startingPosition;
    public Transform targetLocation;

    private bool btriggeredPlatform = false;

    public bool bAutoPlatform = false;
    private bool targetReached = false;
    public float platformSpeed = 3f;

    private void Start()
    {
        startingPosition.GetComponent<MeshRenderer>().enabled = false;
        targetLocation.GetComponent<MeshRenderer>().enabled = false;

        //This is to make sure the platform moves at an even level
        targetLocation.position = new Vector3(targetLocation.position.x, transform.position.y, targetLocation.position.z);
    }

    private void FixedUpdate()
    {
        if(bAutoPlatform && !targetReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, platformSpeed * Time.deltaTime);
            if(transform.position == targetLocation.position)
            {
                targetReached = true;
            }
        }
        else if(bAutoPlatform && targetReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, platformSpeed * Time.deltaTime);
            if (transform.position == startingPosition.position)
            {
                targetReached = false;
            }
        }

        if(!bAutoPlatform && btriggeredPlatform)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, platformSpeed * Time.deltaTime);
        }
        else if(!bAutoPlatform)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, platformSpeed * Time.deltaTime);
        }

    }

    //If not automatic
    public void MovePlatform()
    {
        btriggeredPlatform = true;
    }
    public void ReturnPlatform()
    {
        btriggeredPlatform = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Targetposition(Start)")
        {
            Debug.Log("Start");
            //transform.GetChild()
        }
        
        if(other.transform.name == "Targetposition(Finish)")
        {
            Debug.Log("Finish");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Targetposition(Start)")
        {
            Debug.Log("Start Leave");
        }

        if (other.transform.name == "Targetposition(Finish)")
        {
            Debug.Log("Finish Leave");
        }
    }

}
