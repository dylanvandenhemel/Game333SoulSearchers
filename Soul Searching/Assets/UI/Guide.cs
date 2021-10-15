using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Guide : MonoBehaviour
{
    public int guideInfoNumber;
    public Transform canvas;

    private void Start()
    {
        if(guideInfoNumber > 3 || guideInfoNumber < 0)
        {
            Debug.Log("Guide Info Number must be between 0 - 3");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && guideInfoNumber == 0)
        {
            transform.LookAt(other.transform);
            canvas.GetChild(0).gameObject.SetActive(true);
        }

        if (other.CompareTag("Player") && guideInfoNumber == 1)
        {
            transform.LookAt(other.transform);
            canvas.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.GetChild(0).gameObject.SetActive(false);
            canvas.GetChild(1).gameObject.SetActive(false);
        }
    }
}
