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

    //only 4 buttons at the moment
    private int mainMenuSelection = 5;

    private static bool bDoneTutorial;
    public Text StartGame;

    //Level menu
    public GameObject levelMenu;
        public Text levelSelection;
        private bool blevelOn;
        private int levelMenuSelection;
    //Levels in menu
        public Text tutorial;
      
    //Controller menu
    public GameObject controlMenu;
        public Text controlSelection;
        private bool bcontrolsOn;

    //Settings menu
    public GameObject settingsMenu;
        public Text settings;
        private bool bsettingsOn;
        private int settingSelection;

    public Text quit;


    private void OnEnable()
    {
        menuButtons = new PlayerControls();
        menuButtons.Enable();
        menuButtons.PlayerActions.Possess.performed += SelectUI;
        menuButtons.PlayerActions.MainMenu.started += CurrentSelection;
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.MainMenu.started -= CurrentSelection;
    }

    private void SelectUI(InputAction.CallbackContext c)
    {
        //triggers correct button
        //Start
        if (mainMenuSelection == 4)
        {
            Debug.Log("Start");
            if(!bDoneTutorial)
            {
                bDoneTutorial = true;
                SceneManager.LoadScene("Tutorial");
            }
            else if(bDoneTutorial)
            {
                SceneManager.LoadScene("Level1");
            }
        }
        //LevelSelect
        else if (mainMenuSelection == 3)
        {
            blevelOn = true;
            levelMenu.SetActive(true);

            menuButtons.PlayerActions.Interact.performed += Return;

            //sends player to level selected
            if(blevelOn)
            {
                //tutorial
                if(levelMenuSelection == 1)
                {
                    Debug.Log("load tutorial");
                    levelMenuSelection = 0;
                    SceneManager.LoadScene("Tutorial");
                }
            }
        }
        //Controlls
        else if(mainMenuSelection == 2)
        {
            bcontrolsOn = true;
            controlMenu.SetActive(true);

            menuButtons.PlayerActions.Interact.performed += Return;
        }
        //Settings
        else if(mainMenuSelection == 1)
        {
            bsettingsOn = true;          
            settingsMenu.SetActive(true);

            menuButtons.PlayerActions.Interact.performed += Return;
        }
        //Quit
        else if(mainMenuSelection == 0)
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }

    private void CurrentSelection(InputAction.CallbackContext c)
    {
        if (!bsettingsOn && !blevelOn && !bcontrolsOn)
        {
            //Sets value
            if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().y >= 1)
            {
                //only 4 buttons at the moment
                if (mainMenuSelection < 4)
                {
                    //Debug.Log("Up");
                    mainMenuSelection++;
                }
            }
            else if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().y <= -1)
            {
                if (mainMenuSelection > 0)
                {
                    //Debug.Log("Down");
                    mainMenuSelection--;
                }
            }

            //Updates button selected to color
            if (mainMenuSelection == 4)
            {
                StartGame.color = Color.blue;
            }
            else
            {
                StartGame.color = Color.black;
            }

            if (mainMenuSelection == 3)
            {
                levelSelection.color = Color.blue;
            }
            else
            {
                levelSelection.color = Color.black;
            }

            if (mainMenuSelection == 2)
            {
                controlSelection.color = Color.blue;
            }
            else
            {
                controlSelection.color = Color.black;
            }

            if (mainMenuSelection == 1)
            {
                settings.color = Color.blue;
            }
            else
            {
                settings.color = Color.black;
            }

            if (mainMenuSelection == 0)
            {
                quit.color = Color.red;
            }
            else
            {
                quit.color = Color.black;
            }

        }
        else if (blevelOn)
        {
            //Sets value
            if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().x >= 1)
            {
                if (levelMenuSelection < 1)
                {
                    Debug.Log("Right");
                    levelMenuSelection++;
                }
            }
            else if (menuButtons.PlayerActions.MainMenu.ReadValue<Vector2>().x <= -1)
            {
                if (levelMenuSelection > 0)
                {
                    Debug.Log("Left");
                    levelMenuSelection--;
                }
            }

            //Updates button selections color
            if (levelMenuSelection == 1)
            {
                tutorial.color = Color.blue;
            }
            else
            {
                tutorial.color = Color.black;
            }

        }
        else if (bcontrolsOn)
        {
            //Someday add custom bindings
        }
        else if (bsettingsOn)
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
    }

    public void Return(InputAction.CallbackContext c)
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

        if(bcontrolsOn)
        {
            bcontrolsOn = false;
            controlMenu.SetActive(false);
        }



        menuButtons.PlayerActions.Interact.performed -= Return;
    }

    /* if using mouse

    public void ClickedStart()
    {
        if (!bDoneTutorial)
        {
            bDoneTutorial = true;
            SceneManager.LoadScene("Tutorial");
        }
        else if (bDoneTutorial)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void ClickedLevelSelect()
    {
        blevelOn = true;
        levelMenu.SetActive(true);

        menuButtons.PlayerActions.Interact.performed += Return;
    }

    public void ClickedControls()
    {
        bcontrolsOn = true;
        controlMenu.SetActive(true);

        menuButtons.PlayerActions.Interact.performed += Return;
    }

    public void ClickedSettings()
    {
        bsettingsOn = true;
        settingsMenu.SetActive(true);

        menuButtons.PlayerActions.Interact.performed += Return;
    }
    */

}
