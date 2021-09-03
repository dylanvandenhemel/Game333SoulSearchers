using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startingPosition;
    public Transform targetLocation;

    //Platform Wall Mechanics
    public Transform platWallLeft;
    public Transform platWallRight;
    public Transform platWallUp;
    public Transform platWallDown;

    private bool btriggeredPlatform = false;

    public bool bAutoPlatform = false;
    private bool targetReached = false;
    public float platformSpeed = 3f;

    private void Start()
    {
        startingPosition.GetComponent<MeshRenderer>().enabled = false;
        targetLocation.GetComponent<MeshRenderer>().enabled = false;

        //platWallLeft.GetComponent<MeshRenderer>().enabled = false;
        //platWallRight.GetComponent<MeshRenderer>().enabled = false;
        //platWallUp.GetComponent<MeshRenderer>().enabled = false;
        //platWallDown.GetComponent<MeshRenderer>().enabled = false;
        

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
        if (other.transform.name == "PlatformPort(Left)")
        {
            platWallLeft.GetComponent<Collider>().enabled = false;
        }

        if(other.transform.name == "PlatformPort(Right)")
        {
            platWallRight.GetComponent<Collider>().enabled = false;
        }

        if (other.transform.name == "PlatformPort(Up)")
        {
            platWallUp.GetComponent<Collider>().enabled = false;
        }

        if (other.transform.name == "PlatformPort(Down)")
        {
            platWallDown.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "TargetPosition(Start)")
        {
            platWallLeft.GetComponent<Collider>().enabled = true;
        }

        if (other.transform.name == "PlatformPort(Right)")
        {
            platWallRight.GetComponent<Collider>().enabled = true;
        }

        if (other.transform.name == "PlatformPort(Up)")
        {
            platWallUp.GetComponent<Collider>().enabled = true;
        }

        if (other.transform.name == "PlatformPort(Down)")
        {
            platWallDown.GetComponent<Collider>().enabled = true;
        }
    }

}
