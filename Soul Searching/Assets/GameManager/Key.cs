using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool bghostKey;
    //For Ghost Key Check points
    private void Start()
    {
        if(transform.CompareTag("KeyGhost"))
        {
            bghostKey = true;
        }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.GetComponent<KeyManager>().KeyCollected(transform);
        }

        if(bghostKey && other.CompareTag("GhostDoorCheckPoint"))
        {
            Debug.Log("GDTouch");
            GetComponent<ResetObject>().resetPosition = other.transform.position;
        }
    }

}
