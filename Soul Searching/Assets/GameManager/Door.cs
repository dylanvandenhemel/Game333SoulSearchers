using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && transform.CompareTag("DoorBronze"))
        {
            if(other.GetComponent<KeyManager>().collectedBronzeKey)
            {
                other.GetComponent<UIElements>().BronzeKeyUIOff();

                other.GetComponent<KeyManager>().collectedBronzeKey = false;

                gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("Player") && transform.CompareTag("DoorSilver"))
        {
            if (other.GetComponent<KeyManager>().collectedSilverKey)
            {
                other.GetComponent<UIElements>().SilverKeyUIOff();

                other.GetComponent<KeyManager>().collectedSilverKey = false;

                gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("Player") && transform.CompareTag("DoorGold"))
        {
            if (other.GetComponent<KeyManager>().collectedGoldKey)
            {
                other.GetComponent<UIElements>().GoldKeyUIOff();

                other.GetComponent<KeyManager>().collectedGoldKey = false;

                gameObject.SetActive(false);
            }
        }
        else if(other.CompareTag("Player") && transform.CompareTag("DoorGhost"))
        {
            if (other.GetComponent<KeyManager>().collectedGhostKey)
            {
                other.GetComponent<KeyManager>().collectedGhostKey = false;
                other.GetComponent<KeyManager>().UsedGhostKey();

                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Door");
            }
        }
    }
}
