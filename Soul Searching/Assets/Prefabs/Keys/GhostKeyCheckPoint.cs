using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKeyCheckPoint : MonoBehaviour
{
    private GameObject[] ghostkeyReSpawn;
    void Start()
    {
        ghostkeyReSpawn = GameObject.FindGameObjectsWithTag("KeyGhost");
        GetComponent<MeshRenderer>().enabled = false;
        transform.position = new Vector3(transform.position.x, ghostkeyReSpawn[0].transform.position.y + 0.5f, transform.position.z);
    }
}
