using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Transform attachedDoor;

    private void Start()
    {
        attachedDoor = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.GetComponent<Player>().KeyCollected(attachedDoor);
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponent<Collider>().enabled = false;
        }
    }

}
