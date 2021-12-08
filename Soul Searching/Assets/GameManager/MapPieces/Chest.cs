using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEditor;

public class Chest : MonoBehaviour
{
    PlayerControls pActions;

    private bool bChestOpen;
    public Transform topOpen;

    public GameObject itemInChest;

    private void OnEnable()
    {
        pActions = new PlayerControls();
        pActions.Enable();
    }
    private void OnDisable()
    {
        pActions.Disable();
        pActions.PlayerActions.Interact.performed -= OpenChest;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel)
        {
            pActions.PlayerActions.Interact.performed += OpenChest;
        }

        if(other.CompareTag("Player") && !other.GetComponent<Player>().bpossessSkel)
        {
            pActions.PlayerActions.Interact.performed -= OpenChest;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<Player>().bpossessSkel)
        {
            pActions.PlayerActions.Interact.performed -= OpenChest;
        }
    }

    private void OpenChest(InputAction.CallbackContext c)
    {
        if(!bChestOpen)
        {
            GetComponent<AudioSource>().volume = Settings.masterVolumeSet * Settings.sFXVolumeSet;
            GameObject spawnItem = PrefabUtility.InstantiatePrefab(itemInChest) as GameObject;
            spawnItem.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

            //temporary hide for visual effect
            GetComponent<VisualEffect>().Play();
            topOpen.rotation = Quaternion.Euler(90, topOpen.rotation.y, topOpen.rotation.z);
            bChestOpen = true;
        }
    }
}
