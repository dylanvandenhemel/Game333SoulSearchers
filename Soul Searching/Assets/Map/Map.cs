using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Map : MonoBehaviour
{
    private bool nOpen, eOpen, sOpen, wOpen;

    public MapTileSet mapTile;    

    private float maxDistance = 3;
    public LayerMask Wall;
    RaycastHit hitInfo;
    public void AutoMap()
    {
        
        nOpen = false;
        eOpen = false;
        sOpen = false;
        wOpen = false;
        

        //Detect Walls
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, out hitInfo, maxDistance, Wall))
        {
            Debug.Log("look");
            nOpen = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.red, 1);
        }
        Debug.DrawRay(hitInfo.point, Vector3.up, Color.red, 4);
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



        if (nOpen && eOpen && !sOpen && wOpen) //north dead end
        {
            GameObject nde = PrefabUtility.InstantiatePrefab(mapTile.NDeadEnd) as GameObject;
            nde.transform.position = transform.position;
            nde.transform.parent = transform.parent;
            
        }
        else if (nOpen && eOpen && sOpen && !wOpen) //east dead end
        {
            GameObject ede = PrefabUtility.InstantiatePrefab(mapTile.EDeadEnd) as GameObject;
            ede.transform.position = transform.position;
            ede.transform.parent = transform.parent;
            
        }
        else if (!nOpen && eOpen && sOpen && wOpen) //south dead end
        {
            GameObject sde = PrefabUtility.InstantiatePrefab(mapTile.SDeadEnd) as GameObject;
            sde.transform.position = transform.position;
            sde.transform.parent = transform.parent;
            
        }
        else if (nOpen && !eOpen && sOpen && wOpen) //west dead end
        {
            GameObject wde = PrefabUtility.InstantiatePrefab(mapTile.WDeadEnd) as GameObject;
            wde.transform.position = transform.position;
            wde.transform.parent = transform.parent;
            
        }
                  //Corners
        else if (!nOpen && !eOpen && sOpen && wOpen) //bottem left corner
        {
            GameObject blc = PrefabUtility.InstantiatePrefab(mapTile.BLCorner) as GameObject;
            blc.transform.position = transform.position;
            blc.transform.parent = transform.parent;
            
        }
        else if (!nOpen && eOpen && sOpen && !wOpen) //bottem right corner
        {
            GameObject brc = PrefabUtility.InstantiatePrefab(mapTile.BRCorner) as GameObject;
            brc.transform.position = transform.position;
            brc.transform.parent = transform.parent;
            
        }
        else if (nOpen && !eOpen && !sOpen && wOpen) //top left corner
        {
            GameObject tlc = PrefabUtility.InstantiatePrefab(mapTile.TLCorner) as GameObject;
            tlc.transform.position = transform.position;
            tlc.transform.parent = transform.parent;
            
        }
        else if (nOpen && eOpen && !sOpen && !wOpen) //top right corner
        {
            GameObject trc = PrefabUtility.InstantiatePrefab(mapTile.TRCorner) as GameObject;
            trc.transform.position = transform.position;
            trc.transform.parent = transform.parent;
            
        }
        //hallways
        else if (nOpen && !eOpen && sOpen && !wOpen) // Horizontal Hallway
        {
            GameObject ewh = PrefabUtility.InstantiatePrefab(mapTile.EWHallway) as GameObject;
            ewh.transform.position = transform.position;
            ewh.transform.parent = transform.parent;
            
        }
        else if (!nOpen && eOpen && !sOpen && wOpen) // Vertical hallway
        {
            GameObject nsh = PrefabUtility.InstantiatePrefab(mapTile.NSHallway) as GameObject;
            nsh.transform.position = transform.position;
            nsh.transform.parent = transform.parent;
            
        }
             //1 wall
        else if(nOpen && !eOpen && !sOpen && !wOpen)
        {
            GameObject nw = PrefabUtility.InstantiatePrefab(mapTile.nWall) as GameObject;
            nw.transform.position = transform.position;
            nw.transform.parent = transform.parent;
            ;
        }
        else if(!nOpen && eOpen && !sOpen && !wOpen)
        {
            GameObject ew = PrefabUtility.InstantiatePrefab(mapTile.eWall) as GameObject;
            ew.transform.position = transform.position;
            ew.transform.parent = transform.parent;
            
        }
        else if (!nOpen && !eOpen && sOpen && !wOpen)
        {
            GameObject sw = PrefabUtility.InstantiatePrefab(mapTile.sWall) as GameObject;
            sw.transform.position = transform.position;
            sw.transform.parent = transform.parent;
           
        }
        else if (!nOpen && !eOpen && !sOpen && wOpen)
        {
            GameObject ww = PrefabUtility.InstantiatePrefab(mapTile.wWall) as GameObject;
            ww.transform.position = transform.position;
            ww.transform.parent = transform.parent;
            
        }
        else if(!nOpen && !eOpen && !sOpen && !wOpen)
        {           
            Debug.Log("called");
            GameObject mapSpace = PrefabUtility.InstantiatePrefab(mapTile.space) as GameObject;
            mapSpace.transform.position = transform.position;
            mapSpace.transform.parent = transform.parent;
        }

    }
}
