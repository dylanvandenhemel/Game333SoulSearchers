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

    //logo fade
    public GameObject logoIntro;

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
        public Transform levelPosition;
    Vector3 levelTowerStart;
        Vector3 controlsPosition;
        Quaternion settingRotation;
            Vector3 settingPosition;

    //Level menu
    //public GameObject levelMenu;
        public Transform levelTower;
        public Text levelSelection;
        private bool blevelOn;
        private int levelMenuSelectionX;
        private int levelMenuSelectionY;
        private Vector3 levelCameraAnim;
    //Levels in menu
        //Catacomb
        public Text tutorial;
        public Text level1;
        public Text level2;
        public Text level3;
        public Text level4;
        public Text level5;
        //Dungeon
        public Text level6;
        public Text level7;
        public Text level8;
        //Basement
      
    //Controller menu
    public GameObject controlMenu;
        public Text controlSelection;
        private bool bcontrolsOn;

    //Settings menu
    public GameObject settingsMenu;
        public Text settings;
        private bool bsettingsOn;
        private int settingSelection;
            public GameObject masterVol;
                public Text masterVolText;
            public GameObject musicVol;
                public Text musicVolText;
            public GameObject sFXVol;
            public Text sFXVolText;
    private static float savedMasterVol;
    private static float savedMusicVol;
    private static float savedSFXVol;

    public Text quit;

    public GameObject[] collectablesButton;
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
        menuButtons.PlayerActions.DebugUnlock.started += UnlockLevels;
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Possess.performed -= SelectUI;
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
        menuButtons.PlayerActions.DebugUnlock.started -= UnlockLevels;
    }

    private void UnlockLevels(InputAction.CallbackContext c)
    {
        Debug.Log("All levels Unlocked");
        Settings.levelsUnlocked = 100;
    }

    private void Start()
    {
        mainRotation = transform.rotation;
        mainPosition = transform.position;
        levelRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90, 0);
        levelTowerStart = levelTower.position;
        controlsPosition = new Vector3(transform.position.x - 1.8f, transform.position.y, transform.position.z);
        settingRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y -90, 0);
        settingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.8f);

        savedMasterVol = Settings.masterVolumeSet;
        savedMusicVol = Settings.masterVolumeSet;
        savedSFXVol = Settings.masterVolumeSet;

        if(Settings.masterVolumeSet != 0 && Settings.musicVolumeSet != 0 && Settings.sFXVolumeSet != 0)
        {
            masterVol.GetComponent<UnityEngine.UI.Slider>().value = Settings.masterVolumeSet;
            sFXVol.GetComponent<UnityEngine.UI.Slider>().value = Settings.sFXVolumeSet;
            musicVol.GetComponent<UnityEngine.UI.Slider>().value = Settings.musicVolumeSet;
        }
    }

    private void Update()
    {
        if(animVal == 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, mainRotation, 80 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, mainPosition, 2 * Time.deltaTime);
        }
        else if(animVal == 1)
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, levelRotation, 70 * Time.deltaTime);
            //moves tower in front of camera
            levelTower.position = Vector3.MoveTowards(levelTower.position, levelPosition.position, 3 * Time.deltaTime);
        }
        else if(animVal == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, controlsPosition, 2 * Time.deltaTime);
        }
        else if(animVal == 3)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, settingRotation, 70 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, settingPosition, 2 * Time.deltaTime);
        }

        //moves level tower back into place
        if(animVal != 1)
        {
            levelTower.position = Vector3.MoveTowards(levelTower.position, levelTowerStart, 3 * Time.deltaTime);
        }

        

        //update slider volume levels into game

        Settings.masterVolumeSet = masterVol.GetComponent<UnityEngine.UI.Slider>().value;
        Settings.musicVolumeSet = musicVol.GetComponent<UnityEngine.UI.Slider>().value;
        Settings.sFXVolumeSet = sFXVol.GetComponent<UnityEngine.UI.Slider>().value;


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
                SceneManager.LoadScene(1);
            }
            else if(bDoneTutorial)
            {
                SceneManager.LoadScene(Settings.levelsUnlocked);
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

                //make seperate menu

                //tutorial
                if (levelMenuSelectionX == 1)
                {
                    Debug.Log("load tutorial");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene(1);
                }
                //Level 1
                else if (levelMenuSelectionX == 2)
                {
                    Debug.Log("load level 1");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene(2);
                }
                //Level 2
                else if (levelMenuSelectionX == 3 && Settings.levelsUnlocked > 2)
                {
                    Debug.Log("load level 2");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene(3);
                }
                //Level 3
                else if (levelMenuSelectionX == 4 && Settings.levelsUnlocked > 3)
                {
                    Debug.Log("load level 3");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene(4);
                }
                else if (levelMenuSelectionX == 5 && Settings.levelsUnlocked > 4)
                {
                    Debug.Log("load level 4");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene(5);
                }
                else if (levelMenuSelectionX == 6 && Settings.levelsUnlocked > 5)
                {
                    Debug.Log("load level 5");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene(6);
                }
                else if (levelMenuSelectionX == 7 && Settings.levelsUnlocked > 6)
                {
                    Debug.Log("load level 6");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene(7);
                }
                else if (levelMenuSelectionX == 8 && Settings.levelsUnlocked > 7)
                {
                    Debug.Log("load level 7");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene(8);
                }
                else if (levelMenuSelectionX == 9 && Settings.levelsUnlocked > 8)
                {
                    Debug.Log("load level 8");
                    levelMenuSelectionX = 0;
                    SceneManager.LoadScene(9);
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
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y > 0)
            {
                //only 4 buttons at the moment
                if (mainMenuSelection < 4)
                {
                    //Debug.Log("Up");
                    mainMenuSelection++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y < 0)
            {
                if (mainMenuSelection > 0)
                {
                    //Debug.Log("Down");
                    mainMenuSelection--;
                }
            }
            //changes text color
            TextColor();

        }
        else if (blevelOn)
        {
            //Sets value
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
            {
                if (levelMenuSelectionX < 9)
                {
                    levelMenuSelectionX++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
            {
                if (levelMenuSelectionX > 1)
                {
                    levelMenuSelectionX--;
                }
            }

            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y < 0)
            {
                if (levelMenuSelectionX > 1)
                {
                    //used for the camera movement
                    //levelCameraAnim = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
                    levelMenuSelectionX--;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y > 0)
            {
                if (levelMenuSelectionX < 9)
                {
                    Debug.Log("tdrt");
                    //if(levelMenuSelectionX > 1)
                    //levelCameraAnim = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);

                    
                    levelMenuSelectionX++;
                }
            }
            TextColor();

        }
        else if (bcontrolsOn)
        {
            //Someday add custom bindings
        }
        else if (bsettingsOn)
        {
            //Sets value
            if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y < 0)
            {
                //only -- buttons at the moment
                if (settingSelection < 3)
                {
                    settingSelection++;
                }
            }
            else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().y > 0)
            {
                if (settingSelection > 0)
                {
                    settingSelection--;
                }
            }

            //Updates button selections color
            if (settingSelection == 1)
            {
                masterVolText.color = Color.green;
                if(menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
                {
                    masterVol.GetComponent<UnityEngine.UI.Slider>().value += 0.2f;
                }
                else if(menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
                {
                    masterVol.GetComponent<UnityEngine.UI.Slider>().value -= 0.2f;
                }
            }
            else
            {
                masterVolText.color = Color.magenta;
            }

            if (settingSelection == 2)
            {
                musicVolText.color = Color.green;
                if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
                {
                    musicVol.GetComponent<UnityEngine.UI.Slider>().value += 0.2f;
                }
                else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
                {
                    musicVol.GetComponent<UnityEngine.UI.Slider>().value -= 0.2f;
                }
            }
            else
            {
                musicVolText.color = Color.magenta;
            }

            if (settingSelection == 3)
            {
                sFXVolText.color = Color.green;
                if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
                {
                    sFXVol.GetComponent<UnityEngine.UI.Slider>().value += 0.2f;
                    sFXVol.GetComponent<AudioSource>().Play();
                }
                else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
                {
                    sFXVol.GetComponent<UnityEngine.UI.Slider>().value -= 0.2f;
                    sFXVol.GetComponent<AudioSource>().Play();
                }
                sFXVol.GetComponent<AudioSource>().volume = sFXVol.GetComponent<UnityEngine.UI.Slider>().value;
            }
            else
            {
                sFXVolText.color = Color.magenta;
            }
        }
    }

    private void TextColor()
    {

        //Updates menu button selected to color
        if (mainMenuSelection == 4)
        {
            StartGame.color = Color.green;
        }
        else
        {
            StartGame.color = Color.cyan;
        }

        if (mainMenuSelection == 3)
        {
            levelSelection.color = Color.green;
        }
        else
        {
            levelSelection.color = Color.cyan;
        }

        if (mainMenuSelection == 2)
        {
            controlSelection.color = Color.green;
        }
        else
        {
            controlSelection.color = Color.cyan;
        }

        if (mainMenuSelection == 1)
        {
            settings.color = Color.green;
        }
        else
        {
            settings.color = Color.cyan;
        }

        if (mainMenuSelection == 0)
        {
            quit.color = Color.red;
        }
        else
        {
            quit.color = Color.cyan;
        }

        //Updates level button selections color
        if (levelMenuSelectionX == 1)
        {
            tutorial.color = Color.green;
        }
        else
        {
            tutorial.color = Color.white;
        }

        if (levelMenuSelectionX == 2)
        {
            level1.color = Color.green;
        }
        else
        {
            level1.color = Color.white;
        }

        if (levelMenuSelectionX == 3)
        {
            level2.color = Color.green;
        }
        else if (Settings.levelsUnlocked < 3)
        {
            level2.color = Color.red;
        }
        else
        {
            level2.color = Color.white;
        }

        if (levelMenuSelectionX == 4)
        {
            level3.color = Color.green;
        }
        else if (Settings.levelsUnlocked < 4)
        {
            level3.color = Color.red;
        }
        else
        {
            level3.color = Color.white;
        }

        if (levelMenuSelectionX == 5)
        {
            level4.color = Color.green;
        }
        else if (Settings.levelsUnlocked < 5)
        {
            level4.color = Color.red;
        }
        else
        {
            level4.color = Color.white;
        }

        if (levelMenuSelectionX == 6)
        {
            level5.color = Color.green;
        }
        else if (Settings.levelsUnlocked < 6)
        {
            level5.color = Color.red;
        }
        else
        {
            level5.color = Color.white;
        }

        if (levelMenuSelectionX == 7)
        {
            level6.color = Color.green;
        }
        else if (Settings.levelsUnlocked < 7)
        {
            level6.color = Color.red;
        }
        else
        {
            level6.color = Color.white;
        }

        if (levelMenuSelectionX == 8)
        {
            level7.color = Color.green;
        }
        else if (Settings.levelsUnlocked < 8)
        {
            level7.color = Color.red;
        }
        else
        {
            level7.color = Color.white;
        }

        if (levelMenuSelectionX == 9)
        {
            level8.color = Color.green;
        }
        else if (Settings.levelsUnlocked < 9)
        {
            level8.color = Color.red;
        }
        else
        {
            level8.color = Color.white;
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


        yield return new WaitForSeconds(1);
        if(newMenuVal == 0)
        {
            mainMenu.SetActive(true);
        }
        else if (newMenuVal == 1)
        {
            //old level menu would be enabled
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
