using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void Start()
    {
        GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && transform.CompareTag("DoorBronze"))
        {
            if(other.GetComponent<KeyManager>().collectedBronzeKey)
            {
                other.GetComponent<Player>().pauseMenu.gameObject.GetComponent<UIElements>().BronzeKeyUIOff();

                other.GetComponent<KeyManager>().collectedBronzeKey = false;

                GetComponent<AudioSource>().Play();
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("Player") && transform.CompareTag("DoorSilver"))
        {
            if (other.GetComponent<KeyManager>().collectedSilverKey)
            {
                other.GetComponent<Player>().pauseMenu.gameObject.GetComponent<UIElements>().SilverKeyUIOff();

                other.GetComponent<KeyManager>().collectedSilverKey = false;

                GetComponent<AudioSource>().Play();
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("Player") && transform.CompareTag("DoorGold"))
        {
            if (other.GetComponent<KeyManager>().collectedGoldKey)
            {
                other.GetComponent<Player>().pauseMenu.gameObject.GetComponent<UIElements>().GoldKeyUIOff();

                other.GetComponent<KeyManager>().collectedGoldKey = false;

                GetComponent<AudioSource>().Play();
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else if(other.CompareTag("Player") && transform.CompareTag("DoorGhost"))
        {
            if (other.GetComponent<KeyManager>().collectedGhostKey)
            {
                other.GetComponent<KeyManager>().collectedGhostKey = false;
                other.GetComponent<KeyManager>().UsedGhostKey();

                GetComponent<AudioSource>().Play();
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Door");
            }
        }
    }
}
