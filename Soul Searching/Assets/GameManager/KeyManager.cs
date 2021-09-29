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

    private void OnEnable()
    {
        ResetDelegate.Reset += DropGhostKey;
    }
    private void OnDisable()
    {
        ResetDelegate.Reset -= DropGhostKey;
    }


    public void KeyCollected(Transform currentKey)
    {
        if (currentKey.CompareTag("KeyBronze"))
        {
            GetComponent<UIElements>().BronzeKeyUIOn();

            collectedBronzeKey = true;
            currentKey.gameObject.SetActive(false);
        }
        else if (currentKey.CompareTag("KeySilver"))
        {
            GetComponent<UIElements>().SilverKeyUIOn();

            collectedSilverKey = true;
            currentKey.gameObject.SetActive(false);
        }
        else if (currentKey.CompareTag("KeyGold"))
        {
            GetComponent<UIElements>().GoldKeyUIOn();

            collectedGoldKey = true;
            currentKey.gameObject.SetActive(false);
        }
        else if (currentKey.CompareTag("KeyGhost") && !transform.GetComponent<Player>().bpossessSkel)
        {
            ghostKey = currentKey;
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
