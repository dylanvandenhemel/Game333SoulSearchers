using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Guide : MonoBehaviour
{
    public int guideInfoNumber;
    public Transform canvas;

    private int guideChildCount;

    private void OnEnable()
    {
        ResetDelegate.Reset += ActiveReset;
    }

    private void OnDisable()
    {
        ResetDelegate.Reset -= ActiveReset;
    }

    private void Start()
    {
        guideChildCount = canvas.childCount;
        if(guideInfoNumber > guideChildCount || guideInfoNumber < 0)
        {
            Debug.Log("Guide Info Number must be between 0 - " + (guideChildCount - 1));
        }
    }

    public void ActiveReset()
    {
        for(int i = 0; i < guideChildCount - 1; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        //canvas.GetChild(0).gameObject.SetActive(false);
        //canvas.GetChild(1).gameObject.SetActive(false);
        //canvas.GetChild(2).gameObject.SetActive(false);
        //canvas.GetChild(3).gameObject.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        //hides other UI to show guide
        if (other.CompareTag("Player"))
        {
            // - 2 to leave the pause menu able to pause
            for (int i = 0; i < other.GetComponent<Player>().pauseMenu.transform.childCount - 2; i++)
            {
                other.GetComponent<Player>().pauseMenu.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (other.CompareTag("Player"))
        {
            transform.LookAt(other.transform);
            canvas.GetChild(guideInfoNumber).gameObject.SetActive(true);
        }
        /*
        if (other.CompareTag("Player") && guideInfoNumber == 1)
        {
            transform.LookAt(other.transform);
            canvas.GetChild(1).gameObject.SetActive(true);
        }

        if (other.CompareTag("Player") && guideInfoNumber == 2)
        {
            transform.LookAt(other.transform);
            canvas.GetChild(2).gameObject.SetActive(true);
        }

        if (other.CompareTag("Player") && guideInfoNumber == 3)
        {
            transform.LookAt(other.transform);
            canvas.GetChild(3).gameObject.SetActive(true);
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < guideChildCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        /*
        if (other.CompareTag("Player") && guideInfoNumber == 0)
        {
            canvas.GetChild(0).gameObject.SetActive(false);
        }

        if (other.CompareTag("Player") && guideInfoNumber == 1)
        {
            canvas.GetChild(1).gameObject.SetActive(false);
        }

        if (other.CompareTag("Player") && guideInfoNumber == 2)
        {
            canvas.GetChild(2).gameObject.SetActive(false);
        }

        if (other.CompareTag("Player") && guideInfoNumber == 3)
        {
            transform.LookAt(other.transform);
            canvas.GetChild(3).gameObject.SetActive(false);
        }
        */
    }
}
