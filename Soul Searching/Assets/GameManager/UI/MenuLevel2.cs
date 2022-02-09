using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuLevel2 : MonoBehaviour
{
    PlayerControls menuButtons;

    public GameObject menuCatacombs;
    public GameObject menuDungeons;
    public GameObject menuBasement;

    private bool bisCatacombsMenu;
    private bool bisDungeonsMenu;
    private bool bisBasementMenu;

    public Text[] levelCaticombsList;
    public Text[] levelDungeonsList;
    public Text[] levelBasementList;

    private int currentSelectionVal;

    private int menuMinVal = 0;
    private int menuCatacombsMaxVal;
    private int menuDungeonsMaxVal;
    private int menuBasementMaxVal;

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
        menuCatacombsMaxVal = levelCaticombsList.Length;
        menuDungeonsMaxVal = levelDungeonsList.Length;
        menuBasementMaxVal = levelBasementList.Length;
    }
    private void CurrentSelection(InputAction.CallbackContext c)
    {
        if (bisCatacombsMenu)
        {
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
            {
                if (currentSelectionVal < menuCatacombsMaxVal - 1)
                {
                    currentSelectionVal++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
            {
                if (currentSelectionVal > menuMinVal)
                {
                    currentSelectionVal--;
                }
            }
        }
        else if (bisDungeonsMenu)
        {
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
            {
                if (currentSelectionVal < menuDungeonsMaxVal - 1)
                {
                    currentSelectionVal++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
            {
                if (currentSelectionVal > menuMinVal)
                {
                    currentSelectionVal--;
                }
            }
        }
        else if (bisBasementMenu)
        {
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
            {
                if (currentSelectionVal < menuBasementMaxVal - 1)
                {
                    currentSelectionVal++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
            {
                if (currentSelectionVal > menuMinVal)
                {
                    currentSelectionVal--;
                }
            }
        }

        TextColor(currentSelectionVal);
    }

    private void SelectUI(InputAction.CallbackContext c)
    {
        if(bisCatacombsMenu)
        {
            if (currentSelectionVal == 0)
            {
                SceneManager.LoadScene(2);
            }
            else if (currentSelectionVal == 1)
            {
                SceneManager.LoadScene(3);
            }
            else if (currentSelectionVal == 2)
            {
                SceneManager.LoadScene(4);
            }
            else if (currentSelectionVal == 3)
            {
                SceneManager.LoadScene(5);
            }
            else if (currentSelectionVal == 4)
            {
                SceneManager.LoadScene(6);
            }
            else if (currentSelectionVal == 5)
            {
                //SceneManager.LoadScene(7);
            }
            else if (currentSelectionVal == 6)
            {
                //change dungeon values
            }
            else if (currentSelectionVal == 7)
            {

            }
            else if (currentSelectionVal == 8)
            {

            }
            else if (currentSelectionVal == 9)
            {

            }
        }
        else if (bisDungeonsMenu)
        {
            if (currentSelectionVal == 0)
            {
                SceneManager.LoadScene(7);
            }
            else if (currentSelectionVal == 1)
            {
                SceneManager.LoadScene(8);
            }
            else if (currentSelectionVal == 2)
            {
                SceneManager.LoadScene(9);
            }
            else if (currentSelectionVal == 3)
            {
                SceneManager.LoadScene(10);
            }
            else if (currentSelectionVal == 4)
            {
                SceneManager.LoadScene(11);
            }
            else if (currentSelectionVal == 5)
            {
                SceneManager.LoadScene(12);
            }
            else if (currentSelectionVal == 6)
            {

            }
            else if (currentSelectionVal == 7)
            {

            }
            else if (currentSelectionVal == 8)
            {

            }
            else if (currentSelectionVal == 9)
            {

            }
        }
        else if (bisBasementMenu)
        {
            if (currentSelectionVal == 0)
            {

            }
            else if (currentSelectionVal == 1)
            {

            }
            else if (currentSelectionVal == 2)
            {

            }
            else if (currentSelectionVal == 3)
            {

            }
            else if (currentSelectionVal == 4)
            {

            }
            else if (currentSelectionVal == 5)
            {

            }
            else if (currentSelectionVal == 6)
            {

            }
            else if (currentSelectionVal == 7)
            {

            }
            else if (currentSelectionVal == 8)
            {

            }
            else if (currentSelectionVal == 9)
            {

            }
        }

    }

    //used to deactivate interaction with the menu off screen
    public void subCurrentMenu(int levelMenuIndex)
    {
        //calles here from Menu Animations to determine wich menu is selected
        if(levelMenuIndex == 1)
        {
            menuCatacombs.SetActive(true);
            bisCatacombsMenu = true;
        }
        else if (levelMenuIndex == 2)
        {
            menuDungeons.SetActive(true);
            bisDungeonsMenu = true;
        }
        else if (levelMenuIndex == 3)
        {
            menuBasement.SetActive(true);
            bisBasementMenu = true;
        }
        menuButtons.PlayerActions.Possess.performed += SelectUI;
        menuButtons.PlayerActions.Movement.started += CurrentSelection;
        menuButtons.PlayerActions.Interact.performed += Return;

        TextColor(0);
    }
    public void unSubCurrentMenu(int levelMenuIndex)
    {
        //calles here from Menu Animations to determine witch menu is selected
        if (levelMenuIndex == 1)
        {
            menuCatacombs.SetActive(false);
            bisCatacombsMenu = false;
        }
        else if (levelMenuIndex == 2)
        {
            menuDungeons.SetActive(false);
            bisDungeonsMenu = false;
        }
        else if (levelMenuIndex == 3)
        {
            menuBasement.SetActive(false);
            bisBasementMenu = false;
        }
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
        menuButtons.PlayerActions.Interact.performed -= Return;
    }

    public void Return(InputAction.CallbackContext c)
    {
        GetComponent<MenuAnimation>().cameraAnimCall(1, 0);
    }

    private void TextColor(int menuVal)
    {
        if (bisCatacombsMenu)
        {
            for (int i = 0; i < menuCatacombsMaxVal; i++)
            {
                if (i == menuVal)
                {
                    levelCaticombsList[i].color = Color.blue;
                }
                else
                {
                    levelCaticombsList[i].color = Color.white;
                }
            }
        }
        else if (bisDungeonsMenu)
        {
            for (int i = 0; i < menuDungeonsMaxVal; i++)
            {
                if (i == menuVal)
                {
                    levelDungeonsList[i].color = Color.blue;
                }
                else
                {
                    levelDungeonsList[i].color = Color.white;
                }
            }
        }
        else if (bisBasementMenu)
        {
            for (int i = 0; i < menuBasementMaxVal; i++)
            {
                if (i == menuVal)
                {
                    levelBasementList[i].color = Color.blue;
                }
                else
                {
                    levelBasementList[i].color = Color.white;
                }
            }
        }
    }
}

