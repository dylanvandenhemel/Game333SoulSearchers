using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTower : MonoBehaviour
{
    private bool bTracker;

    public LayerMask Mask;
    private Vector3 fwd;

    private Transform target;

    //private int detectorLimit = 0;

    private void Start()
    {
        fwd = transform.TransformDirection(Vector3.forward);
    }

    /*
    private void FixedUpdate()
    {
        
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
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (bTracker) return;
        bTracker = true;        
        target = other.transform;
        StartCoroutine(nameof(Tracker));
        
    }

    private void OnTriggerExit(Collider other)
    {
        //StopCoroutine(nameof(Tracker));
        bTracker = false;
    }

    IEnumerator Tracker()
    {
        while(bTracker)
        {
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
