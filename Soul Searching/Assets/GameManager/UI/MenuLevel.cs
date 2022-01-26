using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuLevel : MonoBehaviour
{
    PlayerControls menuButtons;
    
    public GameObject menu;

    public Text[] menuItemList;

    public GameObject levelTower;

    private int currentSelectionVal;

    private int menuMinVal = 0;
    private int menuMaxVal = 5;
    
    private void OnEnable()
    {
        menuButtons = new PlayerControls();
        menuButtons.Enable();
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
        menuButtons.PlayerActions.Interact.performed -= Return;
    }
    
    private void Start()
    {
        TextColor(0);
    }
    private void CurrentSelection(InputAction.CallbackContext c)
    {
        //NOTE is reversed because level menu starts at the bottom
        if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y > 0)
        {
            if (currentSelectionVal < menuMaxVal - 1)
            {
                currentSelectionVal++;
                levelTower.transform.position = new Vector3(levelTower.transform.position.x, levelTower.transform.position.y - 0.4f, levelTower.transform.position.z);
            }
        }
        else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y < 0)
        {
            if (currentSelectionVal > menuMinVal)
            {
                currentSelectionVal--;
                levelTower.transform.position = new Vector3(levelTower.transform.position.x, levelTower.transform.position.y + 0.4f, levelTower.transform.position.z);
            }
        }

        TextColor(currentSelectionVal);
    }

    private void SelectUI(InputAction.CallbackContext c)
    {
        if (currentSelectionVal == 0)
        {
            Debug.LogError("Turotial");
        }
        else if (currentSelectionVal == 1)
        {
            Debug.LogError("Catacombs");
        }
        else if (currentSelectionVal == 2)
        {
            Debug.LogError("Dungeons");
        }
        else if (currentSelectionVal == 3)
        {
            Debug.LogError("Basement");
        }
        else if (currentSelectionVal == 4)
        {
            Debug.LogError("House");
        }
    }

    //used to deactivate interaction with the menu off screen
    public void subCurrentSel()
    {
        menuButtons.PlayerActions.Possess.performed += SelectUI;
        menuButtons.PlayerActions.Movement.started += CurrentSelection;
        menuButtons.PlayerActions.Interact.performed += Return;
    }
    public void unSubCurrentSel()
    {
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
        menuButtons.PlayerActions.Interact.performed -= Return;
    }

    public void Return(InputAction.CallbackContext c)
    {
        GetComponent<MenuAnimation>().cameraAnimCall(0);
    }

    private void TextColor(int menuVal)
    {
        for (int i = 0; i < menuMaxVal; i++)
        {
            if (i == menuVal)
            {
                menuItemList[i].color = Color.blue;
            }
            else
            {
                menuItemList[i].color = Color.white;
            }
        }
    }
}
