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
    public GameObject mainMenu;
    private int mainMenuSelection = 5;

    private static bool bDoneTutorial;
    public Text StartGame;

    //animations
    private int animVal;
        Quaternion mainRotation;
            Vector3 mainPosition;
        Quaternion levelRotation;
        Vector3 controlsPosition;
        Quaternion settingRotation;
            Vector3 settingPosition;

    //Level menu
    public GameObject levelMenu;
        public Text levelSelection;
        private bool blevelOn;
        private int levelMenuSelectionX;
        private int levelMenuSelectionY;
    //Levels in menu
        public Text tutorial;
        public Text level1;
        public Text level2;
        public Text level3;
      
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


    private void Awake()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = false;
    }
    //private static bool visible;
    private void OnEnable()
    {
        menuButtons = new PlayerControls();
        menuButtons.Enable();
        menuButtons.PlayerActions.Possess.performed += SelectUI;
        menuButtons.PlayerActions.Movement.started += CurrentSelection;
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
    }

    private void Start()
    {
        mainRotation = transform.rotation;
        mainPosition = transform.position;
        levelRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90, 0);
        controlsPosition = new Vector3(transform.position.x - 1.8f, transform.position.y, transform.position.z);
        settingRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y -90, 0);
        settingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.8f);
    }

    private void Update()
    {
        if(animVal == 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, mainRotation, 50 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, mainPosition, 1 * Time.deltaTime);
        }
        else if(animVal == 1)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, levelRotation, 50 * Time.deltaTime);
        }
        else if(animVal == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, controlsPosition, 1 * Time.deltaTime);
        }
        else if(animVal == 3)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, settingRotation, 50 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, settingPosition, 1 * Time.deltaTime);
        }
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
                //menuButtons.PlayerActions.Possess.performed -= SelectUI;
                //menuButtons.PlayerActions.MainMenu.started -= CurrentSelection;
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
            mainMenu.SetActive(false);
            StartCoroutine(cameraAnimation(1));

            //sends player to level selected
            if(blevelOn)
            {
                //tutorial
                if (levelMenuSelectionX == 1)
                {
                    Debug.Log("load tutorial");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene("Tutorial");
                }
                //Level 1
                else if (levelMenuSelectionX == 2)
                {
                    Debug.Log("load level 1");
                    levelMenuSelectionX = 0;
                    //SceneManager.LoadScene("Tutorial");
                }
                //Level 2
                else if (levelMenuSelectionX == 3)
                {
                    Debug.Log("load level 2");
                    levelMenuSelectionX = 0;
                    //SceneManager.LoadScene("Tutorial");
                }
                //Level 3
                else if (levelMenuSelectionX == 4)
                {
                    Debug.Log("load level 3");
                    levelMenuSelectionX = 0;
                    //SceneManager.LoadScene("Tutorial");
                }
            }
        }
        //Controlls
        else if(mainMenuSelection == 2)
        {
            bcontrolsOn = true;
            mainMenu.SetActive(false);
            StartCoroutine(cameraAnimation(2));
        }
        //Settings
        else if(mainMenuSelection == 1)
        {
            bsettingsOn = true;          
            mainMenu.SetActive(false);
            StartCoroutine(cameraAnimation(3));
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
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y >= 1)
            {
                //only 4 buttons at the moment
                if (mainMenuSelection < 4)
                {
                    //Debug.Log("Up");
                    mainMenuSelection++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y <= -1)
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
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x >= 1)
            {
                if (levelMenuSelectionX < 5)
                {
                    Debug.Log("Right");
                    levelMenuSelectionX++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x <= -1)
            {
                if (levelMenuSelectionX > 0)
                {
                    Debug.Log("Left");
                    levelMenuSelectionX--;
                }
            }

            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y >= 1)
            {
                if (levelMenuSelectionX < 5)
                {
                    Debug.Log("Right");
                    levelMenuSelectionX++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y <= -1)
            {
                if (levelMenuSelectionX > 0)
                {
                    Debug.Log("Left");
                    levelMenuSelectionX--;
                }
            }

            //Updates button selections color
            if (levelMenuSelectionX == 1)
            {
                tutorial.color = Color.blue;
            }
            else
            {
                tutorial.color = Color.black;
            }

            if (levelMenuSelectionX == 2)
            {
                level1.color = Color.blue;
            }
            else
            {
                level1.color = Color.black;
            }

            if (levelMenuSelectionX == 3)
            {
                level2.color = Color.blue;
            }
            else
            {
                level2.color = Color.black;
            }

            if (levelMenuSelectionX == 4)
            {
                level3.color = Color.blue;
            }
            else
            {
                level3.color = Color.black;
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
            levelMenuSelectionX = 0;
            blevelOn = false;
        }

        if(bcontrolsOn)
        {
            bcontrolsOn = false;
            controlMenu.SetActive(false);
        }

        StartCoroutine(cameraAnimation(0));
        menuButtons.PlayerActions.Interact.performed -= Return;
    }

    IEnumerator cameraAnimation(int newMenuVal)
    {
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.Interact.performed -= Return;


        if (newMenuVal == 0)
        {
            animVal = 0;
        }
        else if (newMenuVal == 1)
        {
            animVal = 1;
        }
        else if (newMenuVal == 2)
        {
            animVal = 2;
        }
        else if (newMenuVal == 3)
        {
            animVal = 3;
        }


        yield return new WaitForSeconds(2);
        if(newMenuVal == 0)
        {
            mainMenu.SetActive(true);
        }
        else if (newMenuVal == 1)
        {
            levelMenu.SetActive(true);
        }
        else if (newMenuVal == 2)
        {
            controlMenu.SetActive(true);
        }
        else if (newMenuVal == 3)
        {
            settingsMenu.SetActive(true);
        }

        menuButtons.PlayerActions.Possess.performed += SelectUI;
        menuButtons.PlayerActions.Interact.performed += Return;

    }

}
