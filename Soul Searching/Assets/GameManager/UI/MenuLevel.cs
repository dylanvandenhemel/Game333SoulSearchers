using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuLevel : MonoBehaviour
{
    PlayerControls menuButtons;
    
    public GameObject menu;

    public Text[] menuItemList;

    public GameObject levelTower;

    public int currentSelectionVal;

    private int menuMinVal = 0;
    private int menuMaxVal;
    
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
        menuMaxVal = menuItemList.Length;
        TextColor(0);
    }
    private void CurrentSelection(InputAction.CallbackContext c)
    {
        //NOTE is reversed because level menu starts at the bottom
        if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y > 0)
        {
            if (/*(currentSelectionVal < Settings.levelMenuUnlocked + 1) &&*/ currentSelectionVal < menuMaxVal - 1)
            {
                currentSelectionVal++;
                levelTower.transform.position = new Vector3(levelTower.transform.position.x, levelTower.transform.position.y - 1, levelTower.transform.position.z);
            }
        }
        else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y < 0)
        {
            if (currentSelectionVal > menuMinVal)
            {
                currentSelectionVal--;
                levelTower.transform.position = new Vector3(levelTower.transform.position.x, levelTower.transform.position.y + 1, levelTower.transform.position.z);
            }
        }

        TextColor(currentSelectionVal);
    }

    private void SelectUI(InputAction.CallbackContext c)
    {
        if (currentSelectionVal == 0)
        {
            SceneManager.LoadScene(1);
        }
        else if (currentSelectionVal == 1)
        {
            GetComponent<MenuAnimation>().cameraAnimCall(4, 1);
        }
        else if (currentSelectionVal == 2)
        {
            GetComponent<MenuAnimation>().cameraAnimCall(4, 2);
        }
        else if (currentSelectionVal == 3)
        {
            GetComponent<MenuAnimation>().cameraAnimCall(4, 3);
        }
        else if (currentSelectionVal == 4)
        {
            //SceneManager.LoadScene();
        }
    }

    //used to deactivate interaction with the menu off screen
    public void subCurrentMenu()
    {
        menuButtons.PlayerActions.Possess.performed += SelectUI;
        menuButtons.PlayerActions.Movement.started += CurrentSelection;
        menuButtons.PlayerActions.Interact.performed += Return;
    }
    public void unSubCurrentMenu()
    {
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
        menuButtons.PlayerActions.Interact.performed -= Return;
    }

    public void Return(InputAction.CallbackContext c)
    {
        GetComponent<MenuAnimation>().cameraAnimCall(0, 0);
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
        /*
        for(int i = Settings.levelMenuUnlocked + 2; i < menuMaxVal; i++)
        {
            menuItemList[i].color = Color.red;
        }
        */
    }
}
