using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private Transform ghostKey;
    private float ghostKeyDrop;

    public bool collectedBronzeKey = false;
    public bool collectedSilverKey = false;
    public bool collectedGoldKey = false;

    public bool collectedGhostKey = false;

    public AudioSource keySound;

    private void OnEnable()
    {
        ResetDelegate.Reset += DropGhostKey;
    }
    private void OnDisable()
    {
        ResetDelegate.Reset -= DropGhostKey;
    }

    public void Start()
    {
        keySound.volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
    }


    public void KeyCollected(Transform currentKey)
    {
        if (currentKey.CompareTag("KeyBronze"))
        {
            GetComponent<Player>().pauseMenu.gameObject.GetComponent<UIElements>().BronzeKeyUIOn();

            keySound.Play();
            collectedBronzeKey = true;
            currentKey.gameObject.SetActive(false);

        }
        else if (currentKey.CompareTag("KeySilver"))
        {
            GetComponent<Player>().pauseMenu.gameObject.GetComponent<UIElements>().SilverKeyUIOn();

            keySound.Play();
            collectedSilverKey = true;
            currentKey.gameObject.SetActive(false);
        }
        else if (currentKey.CompareTag("KeyGold"))
        {
            GetComponent<Player>().pauseMenu.gameObject.GetComponent<UIElements>().GoldKeyUIOn();

            keySound.Play();
            collectedGoldKey = true;
            currentKey.gameObject.SetActive(false);
        }
        else if (currentKey.CompareTag("KeyGhost") && !transform.GetComponent<Player>().bpossessSkel)
        {
            ghostKey = currentKey;
            keySound.Play();
            ghostKeyDrop = ghostKey.position.y;
            collectedGhostKey = true;
        }
    }

    public void UsedGhostKey()
    {
        ghostKey.gameObject.SetActive(false);
    }
    private void DropGhostKey()
    {
        collectedGhostKey = false;
    }

    public void Update()
    {
        if(collectedGhostKey)
        {
            if(!transform.GetComponent<Player>().bpossessSkel)
            {
                ghostKey.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }
            else
            {
                ghostKey.position = new Vector3(ghostKey.position.x, ghostKeyDrop, ghostKey.position.z);
                collectedGhostKey = false;
            }
        }
    }
}
