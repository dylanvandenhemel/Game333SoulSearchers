using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTower : MonoBehaviour
{
    private Quaternion startRotation;
    private float viewReternSpeed = 100;

    private bool bTracker = false;
    private Vector3 orgin;
    //private Vector3 fwd;
    private Quaternion targetLook;

    RaycastHit hit;
    public LayerMask Mask;
    public LayerMask Wall;
    private Transform target;

    //private int detectorLimit = 0;

    private void Start()
    {
        startRotation = transform.rotation;
        orgin = transform.position;
    }

    
    private void FixedUpdate()
    {
        if(!bTracker)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, viewReternSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            target = other.transform;
            transform.LookAt(target);

            bTracker = true;
            StartCoroutine(nameof(Tracker));

        }

        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(nameof(Tracker));
            StopTrigger();
            bTracker = false;
        }
            
    }

    IEnumerator Tracker()
    {
        bool calledTrigger = false;
        while (bTracker)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Mask))
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Wall))
                {
                    StopTrigger();
                    bTracker = false;
                }
                else
                {
                    transform.LookAt(target);
                    Debug.DrawRay(orgin, transform.TransformDirection(Vector3.forward) * 10, Color.red, 1);
                    if(calledTrigger == false)
                    {
                        Trigger();
                        calledTrigger = true;
                    }
                }
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
