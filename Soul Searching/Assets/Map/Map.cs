using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]

public class Map : MonoBehaviour
{
    private bool nOpen, eOpen, sOpen, wOpen;

    private Transform nTile, eTile, sTile, wTile;

    public MapTileSet mapTile;

    //Debug to see if placed wrong
    public bool bPlacedTile;

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
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, maxDistance, Wall))
        {
            nOpen = true;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.red, 1);
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) * maxDistance, maxDistance, Wall))
        {
            eOpen = true;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * maxDistance, Color.red, 1);
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back) * maxDistance, maxDistance, Wall))
        {
            sOpen = true;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * maxDistance, Color.red, 1);
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left) * maxDistance, maxDistance, Wall))
        {
            wOpen = true;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * maxDistance, Color.red, 1);
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
            GameObject blc = PrefabUtility.InstantiatePrefab(mapTile.SWCorner) as GameObject;
            blc.transform.position = transform.position;
            blc.transform.parent = transform.parent;
            
        }
        else if (!nOpen && eOpen && sOpen && !wOpen) //bottem right corner
        {
            GameObject brc = PrefabUtility.InstantiatePrefab(mapTile.SECorner) as GameObject;
            brc.transform.position = transform.position;
            brc.transform.parent = transform.parent;
            
        }
        else if (nOpen && !eOpen && !sOpen && wOpen) //top left corner
        {
            GameObject tlc = PrefabUtility.InstantiatePrefab(mapTile.NWCorner) as GameObject;
            tlc.transform.position = transform.position;
            tlc.transform.parent = transform.parent;
            
        }
        else if (nOpen && eOpen && !sOpen && !wOpen) //top right corner
        {
            GameObject trc = PrefabUtility.InstantiatePrefab(mapTile.NECorner) as GameObject;
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
        else if(nOpen && !eOpen && !sOpen && !wOpen) // north wall
        {
            GameObject nw = PrefabUtility.InstantiatePrefab(mapTile.nWall) as GameObject;
            nw.transform.position = transform.position;
            nw.transform.parent = transform.parent;
        }
        else if(!nOpen && eOpen && !sOpen && !wOpen) // east wall
        {
            GameObject ew = PrefabUtility.InstantiatePrefab(mapTile.eWall) as GameObject;
            ew.transform.position = transform.position;
            ew.transform.parent = transform.parent;
            
        }
        else if (!nOpen && !eOpen && sOpen && !wOpen) // south wall
        {
            GameObject sw = PrefabUtility.InstantiatePrefab(mapTile.sWall) as GameObject;
            sw.transform.position = transform.position;
            sw.transform.parent = transform.parent;
           
        }
        else if (!nOpen && !eOpen && !sOpen && wOpen) //west wall
        {
            GameObject ww = PrefabUtility.InstantiatePrefab(mapTile.wWall) as GameObject;
            ww.transform.position = transform.position;
            ww.transform.parent = transform.parent;
            
        }
        else if(!nOpen && !eOpen && !sOpen && !wOpen) //space tile
        {           
            GameObject mapSpace = PrefabUtility.InstantiatePrefab(mapTile.space) as GameObject;
            mapSpace.transform.position = transform.position;
            mapSpace.transform.parent = transform.parent;
        }

    }

    //places pillers in corners
    public void AutoPillar()
    {
        nTile = null;
        eTile = null;
        sTile = null;
        wTile = null;


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, out hitInfo, maxDistance, Wall))
        {
            nTile = hitInfo.transform;            
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) * maxDistance, out hitInfo, maxDistance, Wall))
        {
            eTile = hitInfo.transform;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back) * maxDistance, out hitInfo, maxDistance, Wall))
        {
            sTile = hitInfo.transform;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left) * maxDistance, out hitInfo, maxDistance, Wall))
        {
            wTile = hitInfo.transform;
        }

        //4 CORNERS
        if(nTile != null && eTile != null && sTile != null && wTile != null)
        {
            if((nTile.name == "NDeadEnd" || nTile.name == "NSHallway") && (eTile.name == "EDeadEnd" || eTile.name == "EWHallway") && (sTile.name == "SDeadEnd" || sTile.name == "NSHallway") && (wTile.name == "WDeadEnd" || wTile.name == "EWHallway"))
            {
                GameObject NECorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                NECorner.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1, transform.position.z + 1.5f);
                NECorner.transform.rotation = Quaternion.Euler(0, 0, 0);
                NECorner.transform.parent = transform.parent;

                GameObject NWCorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                NWCorner.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1, transform.position.z + 1.5f);
                NWCorner.transform.rotation = Quaternion.Euler(0, -90, 0);
                NWCorner.transform.parent = transform.parent;

                GameObject SECorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                SECorner.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1, transform.position.z - 1.5f);
                SECorner.transform.rotation = Quaternion.Euler(0, 90, 0);
                SECorner.transform.parent = transform.parent;

                GameObject SWCorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                SWCorner.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1, transform.position.z - 1.5f);
                SWCorner.transform.rotation = Quaternion.Euler(0, 180, 0);
                SWCorner.transform.parent = transform.parent;
            }
        }

        //DOUBLE CORNERS
        //north 2 corners
        else if (nTile != null && eTile != null && wTile != null)
        {
            if ((nTile.name == "NSHallway" || nTile.name == "NDeadEnd") && (eTile.name == "NorthWall" || eTile.name == "EDeadEnd" || eTile.name == "NECorner" || eTile.name == "EWHallway") && (wTile.name == "NorthWall" || wTile.name == "WDeadEnd" || wTile.name == "NWCorner" || wTile.name == "EWHallway"))
            {
                Debug.Log("north corners");
                GameObject NECorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                NECorner.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1, transform.position.z + 1.5f);
                NECorner.transform.rotation = Quaternion.Euler(0, 0, 0);
                NECorner.transform.parent = transform.parent;

                GameObject NWCorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                NWCorner.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1, transform.position.z + 1.5f);
                NWCorner.transform.rotation = Quaternion.Euler(0, -90, 0);
                NWCorner.transform.parent = transform.parent;

                bPlacedTile = true;
            }
        }

        //east 2 corners
        else if (nTile != null && eTile != null && sTile != null)
        {
            if ((nTile.name == "EastWall" || nTile.name == "NDeadEnd" || nTile.name == "NECorner" || nTile.name == "NSHallway") && (eTile.name == "EDeadEnd" || eTile.name == "EWHallway") && (sTile.name == "EastWall" || sTile.name == "SDeadEnd" || sTile.name == "SECorner" || sTile.name == "NSHallway"))
            {
                Debug.Log("east corners");
                GameObject NECorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                NECorner.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1, transform.position.z + 1.5f);
                NECorner.transform.rotation = Quaternion.Euler(0, 0, 0);
                NECorner.transform.parent = transform.parent;

                GameObject SECorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                SECorner.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1, transform.position.z - 1.5f);
                SECorner.transform.rotation = Quaternion.Euler(0, 90, 0);
                SECorner.transform.parent = transform.parent;

                bPlacedTile = true;
            }
        }

        //south 2 corners
        else if (eTile != null && sTile != null && wTile != null)
        {
            if ((eTile.name == "SouthWall" || eTile.name == "EDeadEnd" || eTile.name == "SECorner" || eTile.name == "EWHallway") && (sTile.name == "SDeadEnd" || sTile.name == "NSHallway") && (wTile.name == "SouthWall" || wTile.name == "WDeadEnd" || wTile.name == "SWCorner" || wTile.name == "EWHallway"))
            {
                Debug.Log("south corners");

                GameObject SECorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                SECorner.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1, transform.position.z - 1.5f);
                SECorner.transform.rotation = Quaternion.Euler(0, 90, 0);
                SECorner.transform.parent = transform.parent;

                GameObject SWCorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                SWCorner.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1, transform.position.z - 1.5f);
                SWCorner.transform.rotation = Quaternion.Euler(0, 180, 0);
                SWCorner.transform.parent = transform.parent;

                bPlacedTile = true;
            }
        }

        //west 2 corners
        else if (nTile != null && sTile != null && wTile != null)
        {
            if ((nTile.name == "WestWall" || nTile.name == "NDeadEnd" || nTile.name == "NWCorner" || nTile.name == "NSHallway") && (sTile.name == "WestWall" || sTile.name == "SDeadEnd" || sTile.name == "BottomLeftCorner" || sTile.name == "NSHallway") && (wTile.name == "WDeadEnd" || wTile.name == "EWHallway"))
            {
                Debug.Log("west corners");
                GameObject NWCorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                NWCorner.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1, transform.position.z + 1.5f);
                NWCorner.transform.rotation = Quaternion.Euler(0, -90, 0);
                NWCorner.transform.parent = transform.parent;

                GameObject SWCorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                SWCorner.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1, transform.position.z - 1.5f);
                SWCorner.transform.rotation = Quaternion.Euler(0, 180, 0);
                SWCorner.transform.parent = transform.parent;

                bPlacedTile = true;
            }
        }

        //SINGLE CORNER
        //north east corner
        if (nTile != null && eTile != null)
        {
            if ((nTile.name == "EastWall" || nTile.name == "NDeadEnd" || nTile.name == "NECorner" || nTile.name == "NSHallway") && (eTile.name == "NorthWall" || eTile.name == "EDeadEnd" || eTile.name == "NECorner" || eTile.name == "EWHallway"))
            {
                Debug.Log("Piller north east");
                GameObject NECorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                NECorner.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1, transform.position.z + 1.5f);
                NECorner.transform.rotation = Quaternion.Euler(0, 0, 0);
                NECorner.transform.parent = transform.parent;

                bPlacedTile = true;

            }
        }

        //north west corner
        if (nTile != null && wTile != null)
        {
            if ((nTile.name == "WestWall" || nTile.name == "NDeadEnd" || nTile.name == "NWCorner" || nTile.name == "NSHallway") && (wTile.name == "NorthWall" || wTile.name == "WDeadEnd" || wTile.name == "NWCorner" || wTile.name == "EWHallway"))
            {
                Debug.Log("Piller north west");
                GameObject NWCorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                NWCorner.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1, transform.position.z + 1.5f);
                NWCorner.transform.rotation = Quaternion.Euler(0, -90, 0);
                NWCorner.transform.parent = transform.parent;

                bPlacedTile = true;
            }
        }

        // south east corner
        if (eTile != null && sTile != null)
        {
            if ((sTile.name == "EWall" || sTile.name == "SDeadEnd" || sTile.name == "SECorner" || sTile.name == "NSHallway") && (eTile.name == "SouthWall" || eTile.name == "EDeadEnd" || eTile.name == "SECorner" || eTile.name == "EWHallway"))
            {
                Debug.Log("Piller south east");
                GameObject SECorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                SECorner.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1, transform.position.z - 1.5f);
                SECorner.transform.rotation = Quaternion.Euler(0, 90, 0);
                SECorner.transform.parent = transform.parent;

                bPlacedTile = true;
            }
        }

        // south west corner
        if (sTile != null && wTile != null)
        {
            if ((sTile.name == "WestWall" || sTile.name == "SDeadEnd" || sTile.name == "BottomLeftCorner" || sTile.name == "NSHallway") && (wTile.name == "SouthWall" || wTile.name == "WDeadEnd" || wTile.name == "BottomLeftCorner" || wTile.name == "EWHallway"))
            {
                Debug.Log("Piller south west");
                GameObject SWCorner = PrefabUtility.InstantiatePrefab(mapTile.cornerPiece) as GameObject;
                SWCorner.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1, transform.position.z - 1.5f);
                SWCorner.transform.rotation = Quaternion.Euler(0, 180, 0);
                SWCorner.transform.parent = transform.parent;

                bPlacedTile = true;
            }
        }

    }
}
#endif
