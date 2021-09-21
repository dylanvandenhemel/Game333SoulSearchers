using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Map : MonoBehaviour
{
    private bool nOpen, eOpen, sOpen, wOpen;

    //waits until all open walls are calculated
    private int waitLoadOpens;

    //If only one wall is open
    public GameObject NDeadEnd, EDeadEnd, SDeadEnd, WDeadEnd;

    //If corner
    public GameObject BLCorner, BRCorner, TLCorner, TRCorner;

    //if hallway
    public GameObject NSHallway, EWHallway;

    //if one wall
    public GameObject nWall, eWall, sWall, wWall;


    private float maxDistance = 3;
    public LayerMask Wall;

    private void OnEnable()
    {
        MapGenerator.Generate += AutoMap;
    }
    private void OnDisable()
    {
        MapGenerator.Generate -= AutoMap;
    }

    private void AutoMap()
    {
        //each call it resets for debug purposes
        waitLoadOpens = 0;
        nOpen = false;
        eOpen = false;
        sOpen = false;
        wOpen = false;
        //Detect Walls
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, maxDistance, Wall))
        {
            nOpen = true;
            waitLoadOpens++;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.red, 1);
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) * maxDistance, maxDistance, Wall))
        {
            eOpen = true;
            waitLoadOpens++;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * maxDistance, Color.red, 1);
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back) * maxDistance, maxDistance, Wall))
        {
            sOpen = true;
            waitLoadOpens++;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * maxDistance, Color.red, 1);
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left) * maxDistance, maxDistance, Wall))
        {
            wOpen = true;
            waitLoadOpens++;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * maxDistance, Color.red, 1);
        }


        if (waitLoadOpens == 3)
            if (nOpen && eOpen && wOpen) //north dead end
            {
                PrefabUtility.InstantiatePrefab(NDeadEnd);
                NDeadEnd.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if (nOpen && eOpen && sOpen) //east dead end
            {
                PrefabUtility.InstantiatePrefab(EDeadEnd);
                EDeadEnd.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if (eOpen && sOpen && wOpen) //south dead end
            {
                PrefabUtility.InstantiatePrefab(SDeadEnd);
                SDeadEnd.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if (nOpen && sOpen && wOpen) //west dead end
            {
                PrefabUtility.InstantiatePrefab(WDeadEnd);
                WDeadEnd.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }


        if (waitLoadOpens == 2)
            //Corners
            if (sOpen && wOpen) //bottem left corner
            {
                PrefabUtility.InstantiatePrefab(BLCorner);
                BLCorner.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if (eOpen && sOpen) //bottem right corner
            {
                PrefabUtility.InstantiatePrefab(BRCorner);
                BRCorner.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if (nOpen && wOpen) //top left corner
            {
                PrefabUtility.InstantiatePrefab(TLCorner);
                TLCorner.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if (nOpen && eOpen) //top right corner
            {
                PrefabUtility.InstantiatePrefab(TRCorner);
                TRCorner.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            //hallways
            else if (nOpen && sOpen) // Horizontal Hallway
            {
                PrefabUtility.InstantiatePrefab(EWHallway);
                EWHallway.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if (eOpen && wOpen) // Vertical hallway
            {
                PrefabUtility.InstantiatePrefab(NSHallway);
                NSHallway.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }

        //1 wall
        if (waitLoadOpens == 1)
            if(nOpen)
            {
                PrefabUtility.InstantiatePrefab(nWall);
                nWall.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if(eOpen)
            {
                PrefabUtility.InstantiatePrefab(eWall);
                eWall.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if (sOpen)
            {
                PrefabUtility.InstantiatePrefab(sWall);
                sWall.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }
            else if (wOpen)
            {
                PrefabUtility.InstantiatePrefab(wWall);
                wWall.transform.position = transform.position;
                DestroyImmediate(gameObject);
            }

    }

}
