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
    private int menuSelection = 5;

    public Text StartGame;

    public GameObject levelMenu;
        public Text levelSelection;
        private bool blevelOn;
        private int levelMenuSelection;

    public GameObject controlMenu;
        public Text controlSelection;

    public GameObject settingsMenu;
        public Text settings;
        private bool bsettingsOn;
        private int settingSelection;

    public Text quit;
    //private bool bsettingsOn;
    private void OnEnable()
    {
        menuButtons = new PlayerControls();
        menuButtons.Enable();
        menuButtons.PlayerActions.Possess.performed += SelectUI;
        //menuButtons.PlayerActions.Interact.performed += Return;
        menuButtons.PlayerActions.MainMenu.started += CurrentSelection;
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        //menuButtons.PlayerActions.Interact.performed -= Return;
        menuButtons.PlayerActions.MainMenu.started -= CurrentSelection;
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void SelectUI(InputAction.CallbackContext c)
    {
        //triggers correct button
        //Start
        if (menuSelection == 4)
        {
            Debug.Log("Start");
        }
        //LevelSelect
        else if (menuSelection == 3)
        {
            blevelOn = true;
            levelMenu.SetActive(true);

            menuButtons.PlayerActions.Interact.performed += Return;
        }
        else if(menuSelection == 2)
        {
            Debug.Log("Contrlls");
        }
        //Settings
        else if(menuSelection == 1)
        {
            bsettingsOn = true;          
            settingsMenu.SetActive(true);

            menuButtons.PlayerActions.Interact.performed += Return;
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
        if (!bsettingsOn && !blevelOn)
        {
            //Sets value
            if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().y >= 1)
            {
                //only 4 buttons at the moment
                if (menuSelection < 4)
                {
                    //Debug.Log("Up");
                    menuSelection++;
                }
            }
            else if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().y <= -1)
            {
                if (menuSelection > 0)
                {
                    //Debug.Log("Down");
                    menuSelection--;
                }
            }

            //Updates button selected to color
            if (menuSelection == 4)
            {
                StartGame.color = Color.blue;
            }
            else
            {
                StartGame.color = Color.black;
            }

            if (menuSelection == 3)
            {
                levelSelection.color = Color.blue;
            }
            else
            {
                levelSelection.color = Color.black;
            }

            if (menuSelection == 2)
            {
                controlSelection.color = Color.blue;
            }
            else
            {
                controlSelection.color = Color.black;
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
        else if(bsettingsOn)
        {
            /*Sets value
            if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().x >= 1)
            {
                //only -- buttons at the moment
                if (settingSelection < 1)
                {
                    //Debug.Log("Left");
                    settingSelection++;
                }
            }
            else if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().x <= -1)
            {
                if (settingSelection > 0)
                {
                    //Debug.Log("Right");
                    settingSelection--;
                }
            }
            */
        }
        else if (blevelOn)
        {
            /*Sets value
            if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().x >= 1)
            {
                //only -- buttons at the moment
                if (levelMenuSelection < 1)
                {
                    //Debug.Log("Left");
                    levelMenuSelection++;
                }
            }
            else if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().x <= -1)
            {
                if (levelMenuSelection > 0)
                {
                    //Debug.Log("Right");
                    levelMenuSelection--;
                }
            }
            */
        }
    }



    private void Return(InputAction.CallbackContext c)
    {
        if(bsettingsOn)
        {
            settingsMenu.SetActive(false);
            bsettingsOn = false;
        }

        if(blevelOn)
        {
            levelMenu.SetActive(false);
            blevelOn = false;
        }



        menuButtons.PlayerActions.Interact.performed -= Return;
    }

}
