using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuSelection : MonoBehaviour
{
    PlayerControls menuButtons;
    public Text StartGame;
    public Text levelSelection;
    public Text controlSelection;
    public Text settings;
    public Text quit;

    private int mainMenuSelection = 5;
    private int levelMenuSelectionX;
    private int settingSelection;

    private bool blevelOn;
    private bool bcontrolsOn;
    private bool bsettingsOn;

    public GameObject masterVol;
    public Text masterVolText;
    public GameObject musicVol;
    public Text musicVolText;
    public GameObject sFXVol;
    public Text sFXVolText;

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

    private void OnEnable()
    {
        menuButtons = new PlayerControls();
        menuButtons.Enable();
        menuButtons.PlayerActions.Movement.started += CurrentSelection;
    }
    private void OnDisable()
    {
        menuButtons.Disable();
        menuButtons.PlayerActions.Movement.started -= CurrentSelection;
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
                if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
                {
                    masterVol.GetComponent<UnityEngine.UI.Slider>().value += 0.2f;
                }
                else if (menuButtons.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
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
}
