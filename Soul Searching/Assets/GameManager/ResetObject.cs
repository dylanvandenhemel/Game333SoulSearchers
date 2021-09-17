using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    private Vector3 resetPosition;

    private void Start()
    {
        resetPosition = transform.position;
    }

    private void OnEnable()
    {
        ResetDelegate.Reset += ActiveReset;
    }

    private void OnDisable()
    {
        ResetDelegate.Reset -= ActiveReset;
    }

    private void ActiveReset()
    {
        Debug.Log("Reset");
        transform.position = resetPosition;
    }
}