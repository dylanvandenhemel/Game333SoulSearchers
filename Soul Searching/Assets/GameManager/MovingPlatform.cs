using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform targetLocation;
    private Vector3 startingPosition;

    private bool btriggeredPlatform = false;

    public bool bAutoPlatform = false;
    private bool targetReached = false;
    public float platformSpeed = 3f;

    private void Start()
    {
        targetLocation.GetComponent<MeshRenderer>().enabled = false;

        startingPosition = transform.position;

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
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, platformSpeed * Time.deltaTime);
            if (transform.position == startingPosition)
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
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, platformSpeed * Time.deltaTime);
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

    //TO DO: ATTEMT TO MAKE PHYSICAL/BONES(EXCLUSIVE) CHILD OF PLATFORM SO IT MOVES WITH IT
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Physical") || other.gameObject.layer == LayerMask.NameToLayer("Bones(Exclusive)"))
        {
            Debug.Log("Become Platform");
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Physical") || other.gameObject.layer == LayerMask.NameToLayer("Bones(Exclusive)"))
        {
            Debug.Log("No more Platform");
            other.transform.parent = null;
        }
    }
}
