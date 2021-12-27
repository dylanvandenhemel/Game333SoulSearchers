using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EyeTower : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask Mask;
    public LayerMask Wall;
    private Transform target;
    private float playerDistance = 10;

    //wether it pans 180 or 360 degrees
    public bool bPanningOn = true;
    public bool bselect180 = false;
    public bool bselect360 = false;
    private float panRotationDegrees = 80f;
    private bool bPanned = false;
    public float panSpeed = 50f;
    private Quaternion panLeft;
    private Quaternion panRight;
    private Quaternion startRotation;
    private float viewReternSpeed;

    private bool btriggerActivated;
    private bool bTracker = false;
    private Vector3 orgin;

    private bool beyeSoundPlayed;
    private void OnEnable()
    {
        ResetDelegate.Reset += ActiveReset;
    }

    private void OnDisable()
    {
        ResetDelegate.Reset -= ActiveReset;
    }

    private void Start()
    {
        viewReternSpeed = panSpeed;
        startRotation = transform.rotation;
        orgin = transform.position;

        if(bPanningOn)
        { 
            panLeft = Quaternion.Euler(0, transform.rotation.eulerAngles.y + panRotationDegrees, 0);
            panRight = Quaternion.Euler(0, transform.rotation.eulerAngles.y - panRotationDegrees, 0);
        }

        GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
    }

    
    private void FixedUpdate()
    {
        if(bselect180)
        {
            Pan180Degrees();
        }
        else if(bselect360)
        {
            Pan360Degrees();
        }
        else if (!bTracker && !bselect180 && !bselect360)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, viewReternSpeed * Time.deltaTime);
        }

    }


    public void Pan180Degrees()
    {
        if (!bTracker && !bPanningOn)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, viewReternSpeed * Time.deltaTime);
        }
        else if (!bTracker && bPanningOn)
        {
            if (!bPanned)
            {
                if (transform.rotation == panLeft)
                {
                    bPanned = true;
                }
                transform.rotation = Quaternion.RotateTowards(transform.rotation, panLeft, panSpeed * Time.deltaTime);

            }
            else if (bPanned)
            {
                if (transform.rotation == panRight)
                {
                    bPanned = false;

                }
                transform.rotation = Quaternion.RotateTowards(transform.rotation, panRight, panSpeed * Time.deltaTime);

            }
        }
    }

    public void Pan360Degrees()
    {
        transform.Rotate(0, panSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            transform.LookAt(target);
            playerDistance = hit.distance;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 6, Mask))
            {
                bTracker = true;
                if (bTracker == true && !btriggerActivated)
                {
                    transform.LookAt(target);
                    Debug.DrawRay(orgin, transform.TransformDirection(Vector3.forward) * 10, Color.green, 1);

                    btriggerActivated = true;
                    Trigger();
                    //StartCoroutine(triggerWait());
                }
            }

        }


    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && btriggerActivated)
        {
            Trigger();
            StartCoroutine(triggerWait());
        }
            
    }

    
    IEnumerator triggerWait()
    {
        yield return new WaitForSeconds(0.5f);
        bTracker = false;
        btriggerActivated = false;
    }
    
    public void Trigger()
    {
        if(!beyeSoundPlayed)
        {
            GetComponent<AudioSource>().Play();
            beyeSoundPlayed = true;
        }
        else
        {
            beyeSoundPlayed = false;
        }
        transform.GetComponent<Activator>().Trigger();
    }

    public void ActiveReset()
    {
        if(bTracker)
        {
            Trigger();
            btriggerActivated = false;
        }
        bTracker = false;
    }
}
