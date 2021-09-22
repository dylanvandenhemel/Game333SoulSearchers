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
        //waitLoadOpens = 0;
        nOpen = false;
        eOpen = false;
        sOpen = false;
        wOpen = false;
        //Detect Walls
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, maxDistance, Wall))
        {
            nOpen = true;
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


        //if (waitLoadOpens == 3)
            if (nOpen && eOpen && wOpen) //north dead end
            {
                GameObject nde = PrefabUtility.InstantiatePrefab(NDeadEnd) as GameObject;
                nde.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if (nOpen && eOpen && sOpen) //east dead end
            {
                GameObject ede = PrefabUtility.InstantiatePrefab(EDeadEnd) as GameObject;
                ede.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if (eOpen && sOpen && wOpen) //south dead end
            {
                GameObject sde = PrefabUtility.InstantiatePrefab(SDeadEnd) as GameObject;
                sde.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if (nOpen && sOpen && wOpen) //west dead end
            {
                GameObject wde = PrefabUtility.InstantiatePrefab(WDeadEnd) as GameObject;
                wde.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }


        //if (waitLoadOpens == 2)
            //Corners
            if (sOpen && wOpen) //bottem left corner
            {
                GameObject blc = PrefabUtility.InstantiatePrefab(BLCorner) as GameObject;
                blc.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if (eOpen && sOpen) //bottem right corner
            {
                GameObject brc = PrefabUtility.InstantiatePrefab(BRCorner) as GameObject;
                brc.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if (nOpen && wOpen) //top left corner
            {
                GameObject tlc = PrefabUtility.InstantiatePrefab(TLCorner) as GameObject;
                tlc.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if (nOpen && eOpen) //top right corner
            {
                GameObject trc = PrefabUtility.InstantiatePrefab(TRCorner) as GameObject;
                trc.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            //hallways
            else if (nOpen && sOpen) // Horizontal Hallway
            {
                GameObject ewh = PrefabUtility.InstantiatePrefab(EWHallway) as GameObject;
                ewh.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if (eOpen && wOpen) // Vertical hallway
            {
                GameObject nsh = PrefabUtility.InstantiatePrefab(NSHallway) as GameObject;
                nsh.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }

        //1 wall
        //if (waitLoadOpens == 1)
            if(nOpen)
            {
                GameObject nw = PrefabUtility.InstantiatePrefab(nWall) as GameObject;
                nw.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if(eOpen)
            {
                GameObject ew = PrefabUtility.InstantiatePrefab(eWall) as GameObject;
                ew.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if (sOpen)
            {
                GameObject sw = PrefabUtility.InstantiatePrefab(sWall) as GameObject;
                sw.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }
            else if (wOpen)
            {
                GameObject ww = PrefabUtility.InstantiatePrefab(wWall) as GameObject;
                ww.transform.position = transform.position;
                //DestroyImmediate(gameObject);
            }

    }

}
