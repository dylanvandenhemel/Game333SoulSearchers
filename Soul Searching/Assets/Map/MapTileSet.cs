using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = ("TileSet"))]
public class MapTileSet : ScriptableObject
{
    public GameObject space;

    public GameObject cornerPiece;
    //If only one wall is open
    public GameObject NDeadEnd, EDeadEnd, SDeadEnd, WDeadEnd;

    //If corner
    public GameObject SWCorner, SECorner, NECorner, NWCorner;

    //if hallway
    public GameObject NSHallway, EWHallway;

    //if one wall
    public GameObject nWall, eWall, sWall, wWall;
}
