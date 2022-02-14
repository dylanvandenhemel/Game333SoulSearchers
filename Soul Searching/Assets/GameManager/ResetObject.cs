using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    [HideInInspector]public Vector3 resetPosition;
    [HideInInspector]public bool bUponReset;

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
        transform.position = resetPosition;
        bUponReset = true;
        StartCoroutine(resetTimer());
    }

    IEnumerator resetTimer()
    {
        yield return new WaitForSeconds(1);
        bUponReset = false;
    }
}
