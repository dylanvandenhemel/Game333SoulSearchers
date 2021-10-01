using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    PlayerControls menuButtons;
    private Scene currentScene;

    //only 4 buttons at the moment
    private int menuSelection = 4;

    public Text StartGame;

    public Text levelSelection;
    //public GameObject levelSelectionMenu;

    public GameObject settingsMenu;
        public Text settings;
        public Text settingBack;
        private bool bsettingsOn;
        private int settingSelection;

    public Text quit;
    //private bool bsettingsOn;
    private void OnEnable()
    {
        menuButtons = new PlayerControls();
        menuButtons.Enable();
        menuButtons.PlayerActions.Possess.performed += SelectUI;
        menuButtons.PlayerActions.Movement.performed += CurrentSelection;
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.Movement.performed -= CurrentSelection;
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void SelectUI(InputAction.CallbackContext c)
    {
        //triggers correct button
        //Start
        if(menuSelection == 3)
        {
            Debug.Log("Start");
        }
        //LevelSelect
        else if(menuSelection == 2)
        {
            Debug.Log("Level");
        }
        //Settings
        else if(menuSelection == 1)
        {
            menuButtons.PlayerActions.Movement.performed -= CurrentSelection;
            bsettingsOn = true;          
            Debug.Log("sett" + bsettingsOn);
            settingsMenu.SetActive(true);
        }
        //Quit
        else if(menuSelection == 0)
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }

    private void CurrentSelection(InputAction.CallbackContext c)
    {
        Debug.Log(bsettingsOn);
        if (!bsettingsOn)
        {
            //Sets value
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y > 0)
            {
                //only 4 buttons at the moment
                if (menuSelection < 3)
                {
                    //Debug.Log("Up");
                    menuSelection++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y < 0)
            {
                if (menuSelection > 0)
                {
                    //Debug.Log("Down");
                    menuSelection--;
                }
            }

            //Updates button selected to color
            if (menuSelection == 3)
            {
                StartGame.color = Color.blue;
            }
            else
            {
                StartGame.color = Color.black;
            }

            if (menuSelection == 2)
            {
                levelSelection.color = Color.blue;
            }
            else
            {
                levelSelection.color = Color.black;
            }

            if (menuSelection == 1)
            {
                settings.color = Color.blue;
            }
            else
            {
                settings.color = Color.black;
            }

            if (menuSelection == 0)
            {
                quit.color = Color.blue;
            }
            else
            {
                quit.color = Color.black;
            }

        }
    }



    private void Settings(InputAction.CallbackContext c)
    {
        //settingsMenu.SetActive(false);

        //menuButtons.PlayerActions.Interact.performed -= Settings;
        //menuButtons.PlayerActions.Possess.performed += CurrentSelection;

        /*
        //Sets value
        if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
        {
            //only -- buttons at the moment
            if (settingSelection < --)
            {
                //Debug.Log("Left");
                settingSelection++;
            }
        }
        else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
        {
            if (settingSelection > --)
            {
                //Debug.Log("Right");
                settingSelection--;
            }
        }
        */

    }

}
