using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoors : MonoBehaviour
{
    private GameObject attachedKey;

    private void Start()
    {
        attachedKey = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(attachedKey.name);
    }
}
