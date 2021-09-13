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

    //wether it pans 180 or 360 degrees
    public bool bPanningOn = false;
    private bool bselect180 = false;
    private bool bselect360 = false;
    private float panRotationDegrees = 80f;
    private bool bPanned = false;
    private float panSpeed = 50f;
    private Quaternion panLeft;
    private Quaternion panRight;
    private Quaternion startRotation;
    private float viewReternSpeed = 50f;

    private bool bTracker = false;
    private Vector3 orgin;

    private void Start()
    {
        startRotation = transform.rotation;
        orgin = transform.position;

        if(bPanningOn)
        { 
            panLeft = Quaternion.Euler(0, transform.rotation.eulerAngles.y + panRotationDegrees, 0);
            panRight = Quaternion.Euler(0, transform.rotation.eulerAngles.y - panRotationDegrees, 0);
        }
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

    }

    public void Selected180()
    {
        bselect180 = true;
    }
    public void Selected360()
    {
        bselect360 = true;
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
        if(other.CompareTag("Player"))
        {
            if (other.CompareTag("Player"))
            {
                target = other.transform;
                transform.LookAt(target);

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Mask))
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Wall))
                    {
                        
                        StopTrigger();
                        
                    }
                    else if (bTracker == true)
                    {
                        transform.LookAt(target);
                        Debug.DrawRay(orgin, transform.TransformDirection(Vector3.forward) * 10, Color.green, 1);
                        Trigger();
                    }
                    bTracker = true;
                }

            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopTrigger();
            bTracker = false;
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


//Edits unity Inspector to show or hide options
[ExecuteInEditMode]
[CustomEditor(typeof(EyeTower))]
public class EyeTowerCI : Editor
{
    public bool b180Rotation = false;
    public bool b360Rotation = false;
    enum eyeRotations {b180Rotation, b360Rotation};
    public override void OnInspectorGUI()
    {
        var myScript = (target as EyeTower);
        base.OnInspectorGUI();
        if(myScript.bPanningOn)
        {
            EditorGUILayout.LabelField("Choose Rotation");
            b180Rotation = GUILayout.Toggle(b180Rotation, "180 Rotation");
            b360Rotation = GUILayout.Toggle(b360Rotation, "360 Rotation");

            if (b180Rotation)
            {
                myScript.Selected180();

            }
            else if (b360Rotation)
            {
                myScript.Selected360();

            }
        }

        
    }
}

