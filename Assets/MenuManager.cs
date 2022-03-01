using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    /*
     * COMP280 NOTE
     * 
     * This script was rewritten for comp280 worksheet 4 (Human-Computer interaction)
     * The script was originally built for handling the inventory and will now be used to handle all of the UI outside of combat 
     * The original code was >1000 lines so it was rewritten for maintainability reasons
     * All of the code has been rewritten to make sure it worked 100% for both GAM220 and COMP280
     */

    #region GameObjects

    PlayerController playerController;
    LoadSettings loadSettings;

    public GameObject ExplorationUI;
    public GameObject PauseMenuUI;
    public GameObject Player;
    public GameObject PlayerCamera;
    public Compass compass;

    public GameObject SettingsMenu;
    public GameObject HelpMenu;
    public GameObject QuestMenuUI;
    public GameObject GuideBook;

    public GameObject MainMenuConfirmScreen;
    public GameObject QuitConfirmScreen;

    public Slider PauseHealthBar;
    public Slider PauseArcanaBar;
    public Text HPPotionCount;
    public Text APPotionCount;
    public Text RPotionCount;
    public Text SPotionCount;

    public Text goldCount;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Pausemenu UI OFF
        PauseMenuUI.SetActive(false);
        //Exploration UI ON
        ExplorationUI.SetActive(true);
        //hide/lock mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        loadSettings = LoadSettings.instance;
    }

    // Update is called once per frame
    public void Update()
    {
        #region Open Hotkeys
        //J to OPEN quest menu
        if (Input.GetKeyDown(KeyCode.J) && PauseMenuUI.activeSelf == false)
        {
            //turn off Exploration UI
            ExplorationUI.SetActive(false);
            //turn on Quest Menu UI
            QuestMenuUI.SetActive(true);

            //freeze player/camera
            Player.GetComponent<PlayerController>().canMove = false;
            //unlock mouse - confined to window
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            //debug
            Debug.Log("Quest Menu Active");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenuUI.activeSelf == true)
            {
                PauseMenuUI.SetActive(false);
                ExplorationUI.SetActive(true);
                //freeze player/camera
                Player.GetComponent<PlayerController>().canMove = true;
                //unlock mouse - confined to window
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (QuestMenuUI.activeSelf == true)
            {
                QuestMenuUI.SetActive(false);
                ExplorationUI.SetActive(true);
                //freeze player/camera
                Player.GetComponent<PlayerController>().canMove = true;
                //unlock mouse - confined to window
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                PauseMenuUI.SetActive(true);
                ExplorationUI.SetActive(false);
                //freeze player/camera
                Player.GetComponent<PlayerController>().canMove = false;
                //unlock mouse - confined to window
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        #endregion

        #region Stats Update
        if(PauseMenuUI.activeSelf == true)
        {
            //update the HP and AP bars
            PauseHealthBar.value = loadSettings.health;
            Debug.Log(PauseHealthBar.value + "||" + loadSettings.health);
            //PauseArcanaBar.value = loadSettings.arcana;

            //update gold counter
            goldCount.text = loadSettings.currentGold.ToString();

            //update the number of all the potions
            HPPotionCount.text = loadSettings.potionCount.ToString();
            APPotionCount.text = loadSettings.potionCount.ToString();
            RPotionCount.text = loadSettings.potionCount.ToString();
            SPotionCount.text = loadSettings.potionCount.ToString();
        }
        #endregion
    }

    #region Pause Menu Buttons

    #region Resume Button
    public void Resume()
    {
        //turn on Exploration UI
        ExplorationUI.SetActive(true);
        //turn off Pause Menu UI
        PauseMenuUI.SetActive(false);
        //Unfreeze player and camera
        Player.GetComponent<PlayerController>().canMove = true;
        //hide mouse/lock in place
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region Settings Button
    public void Settings()
    {
        //turn off pause menu UI
        PauseMenuUI.SetActive(false);
        //turn on Settings UI
        SettingsMenu.SetActive(true);
    }
    #endregion

    #region Help Button
    public void Help()
    {
        //turn off pause menu UI
        PauseMenuUI.SetActive(false);
        //turn on GuideBook UI
        HelpMenu.SetActive(true);
    }
    #endregion

    #region MainMenu Button
    public void MainMenu()
    {
        //Load Confirm Screen
        MainMenuConfirmScreen.SetActive(true);

    }
    public void MainMenuConfirm()
    {
        //Return to main menu
        SceneManager.LoadScene("Splash Screen");
    }
    public void MainMenuDeny()
    {
        //Close Quit confirmation screen
        MainMenuConfirmScreen.SetActive(false);
    }
    #endregion

    #region CloseGame Button
    public void CloseGame()
    {
        //Load confirm screen
        QuitConfirmScreen.SetActive(true);
    }

    public void CloseConfirm()
    {
        //Quit Game
        Application.Quit();
        //Debug
        Debug.Log("Closed Game");
    }

    public void CloseDeny()
    {
        //Close Quit confirmation screen
        QuitConfirmScreen.SetActive(false);
    }
    #endregion

    #region DeckBuilder Button
    public void DeckBuilder()
    {
        //turn off pause menu UI
        //turn on deck builder UI
    }
    #endregion

    #region Close Sub-Menu Button
    public void CloseSubMenu()
    {
        //close settings/guidebook menus
        SettingsMenu.SetActive(false);
        GuideBook.SetActive(false);
        //open pausemenu UI
        PauseMenuUI.SetActive(true);
        //debug
        Debug.Log("Closed Sub Menu");
    }
    #endregion

    #endregion
}