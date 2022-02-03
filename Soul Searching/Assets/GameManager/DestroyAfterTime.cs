using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private const float destroyTime = 5f;
    void Start()
    {
        StartCoroutine("Destroy");
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
