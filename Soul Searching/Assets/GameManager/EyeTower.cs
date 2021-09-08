using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTower : MonoBehaviour
{
    //private float debugtimer = 10;

    private bool bTracker = false;
    private Vector3 orgin;
    //private Vector3 fwd;
    private Quaternion targetLook;


    public LayerMask Mask;
    private Transform target;

    //private int detectorLimit = 0;

    private void Start()
    {
        orgin = transform.position;
        //fwd = transform.TransformDirection(Vector3.forward);
    }

    
    private void FixedUpdate()
    {
        //Debug.DrawRay(orgin, fwd * 10, Color.red);
        /*
        if(Physics.Raycast(transform.position, fwd, Mathf.Infinity, Mask))
        {
            if(detectorLimit == 1)
            {
                Trigger();
                detectorLimit = 0;
            }
        }
        else
        {
            if(detectorLimit == 0)
            {
                StopTrigger();
                detectorLimit = 1;
            }
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            bTracker = true;

            target = other.transform;
            StartCoroutine(nameof(Tracker));
        }

        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(nameof(Tracker));
        }
            
    }

    IEnumerator Tracker()
    {
        RaycastHit hit;
        while (bTracker)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Mask))
            {
                transform.LookAt(target);
                Debug.DrawRay(orgin, transform.TransformDirection(Vector3.forward) * 10, Color.red, 1);

            }


            yield return new WaitForEndOfFrame();
        }
    }

    public void Trigger()
    {
        transform.GetComponent<Activator>().Trigger();
    }

    public void StopTrigger()
    {
        transform.GetComponent<Activator>().StopTrigger();
    }
}
