using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Map : MonoBehaviour
{
    private bool triggered;
    private bool nOpen, eOpen, sOpen, wOpen;

    //If only one wall open
    public GameObject nWall, eWall, sWall, wWall;

    //If two walls open

    //If 3 walls open

    //If all open


    private float maxDistance = 3;
    public LayerMask Wall;


    private void FixedUpdate()
    {
        //Detect Walls
        if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, maxDistance, Wall))
        {
            nOpen = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.red, 1);
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) * maxDistance, maxDistance, Wall))
        {
            eOpen = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * maxDistance, Color.red, 1);
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back) * maxDistance, maxDistance, Wall))
        {
            sOpen = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * maxDistance, Color.red, 1);
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left) * maxDistance, maxDistance, Wall))
        {
            wOpen = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * maxDistance, Color.red, 1);
        }

        //After wall detection just for debug
        if(nOpen)
        {
            Debug.Log("no n wall");
        }

        if (eOpen)
        {
            Debug.Log("no e wall");
        }

        if (sOpen)
        {
            Debug.Log("no s wall");
        }

        if (wOpen)
        {
            Debug.Log("no w wall");
        }
        //^^^^^ just for debug


        if(!triggered)
        {
            if (eOpen && sOpen && wOpen) //north no wall
            {
                PrefabUtility.InstantiatePrefab(nWall);
                nWall.transform.position = transform.position;
            }
            else if(sOpen && wOpen && nOpen) //east no wall
            {
                PrefabUtility.InstantiatePrefab(eWall);
                eWall.transform.position = transform.position;
            }
            else if(wOpen && nOpen && eOpen) //south no wall
            {
                PrefabUtility.InstantiatePrefab(sWall);
                sWall.transform.position = transform.position;
            }
            else if (nOpen && eOpen && sOpen) //west no wall
            {
                PrefabUtility.InstantiatePrefab(wWall);
                wWall.transform.position = transform.position;
            }
            triggered = true;

        }
    }









}
