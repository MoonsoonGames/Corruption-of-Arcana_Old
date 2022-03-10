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
    public GameObject QuestMenuUI;
    public GameObject GuideBook;
    public GameObject CardsMenu;

    public GameObject MainMenuConfirmScreen;
    public GameObject QuitConfirmScreen;

    public Slider PauseHealthBar;
    public Slider PauseArcanaBar;
    public Text HPPotionCount;
    public Text APPotionCount;
    //public Text RPotionCount;
    //public Text SPotionCount;

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
        if (Player.GetComponent<PlayerController>().canMove == true)
        {
            #region Esc open
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //open pause
                PauseMenuUI.SetActive(true);
                //close explore
                ExplorationUI.SetActive(false);

                //freeze player
                Player.GetComponent<PlayerController>().canMove = false;

                //unlock mouse - confined to window
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                //debug
                Debug.Log("Open Pause menu");
            }
            #endregion

            #region J open
            if (Input.GetKeyDown(KeyCode.J) && PauseMenuUI.activeSelf == false)
            {
                //open quest
                QuestMenuUI.SetActive(true);
                //close explore
                ExplorationUI.SetActive(false);

                //freeze player/camera
                Player.GetComponent<PlayerController>().canMove = false;

                //unlock mouse - confined to window
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;

                //debug
                Debug.Log("Open Quest menu");
            }
            #endregion
        }
        else if(Player.GetComponent<PlayerController>().canMove == false)
        {
            #region Esc close
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PauseMenuUI.activeSelf == true || QuestMenuUI.activeSelf == true)
                {
                    //close pause
                    PauseMenuUI.SetActive(false);
                    //close quests
                    QuestMenuUI.SetActive(false);
                    //close submenus
                    MainMenuConfirmScreen.SetActive(false);
                    QuitConfirmScreen.SetActive(false);
                    //open explore
                    ExplorationUI.SetActive(true);

                    //unfreeze player/camera
                    Player.GetComponent<PlayerController>().canMove = true;
                    //hide mouse - lock to centre
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                    //debug
                    Debug.Log("Close Pause menu");
                }
                else if (SettingsMenu.activeSelf == true || GuideBook.activeSelf == true || CardsMenu.activeSelf == true)
                {
                    //close settings
                    SettingsMenu.SetActive(false);
                    //close guide book
                    GuideBook.SetActive(false);
                    //close cards
                    CardsMenu.SetActive(false);
                    //open pause
                    PauseMenuUI.SetActive(true);

                    //unfreeze player/camera
                    Player.GetComponent<PlayerController>().canMove = false;
                    //hide mouse - unlocked
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    //debug
                    Debug.Log("Close Quest menu");
                }
            }
            #endregion
        }

        #region Stats Update
        //update the HP and AP bars
        PauseHealthBar.value = loadSettings.health;
        PauseHealthBar.maxValue = loadSettings.maxHealth;
        //PauseArcanaBar.value = loadSettings.arcana;
        //PauseArcanaBar.maxValue = loadSettings.maxArcana;

        Debug.Log(PauseHealthBar.value + "||" + loadSettings.health);

        //update gold counter
        goldCount.text = loadSettings.currentGold.ToString();

        //update the number of all the potions
        HPPotionCount.text = loadSettings.healingPotionCount.ToString();
        APPotionCount.text = loadSettings.arcanaPotionCount.ToString();
        //RPotionCount.text = loadSettings.potionCount.ToString();
        //SPotionCount.text = loadSettings.potionCount.ToString();
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
        GuideBook.SetActive(true);
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
        SceneManager.LoadScene("SplashScreen");
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

    #region Card Menu Button
    public void DeckBuilder()
    {
        //turn off pause menu UI
        PauseMenuUI.SetActive(false);
        //turn on deck builder UI
        CardsMenu.SetActive(true);
    }
    #endregion

    #region Close Sub-Menu Button
    public void CloseSubMenu()
    {
        //close settings/guidebook menus
        SettingsMenu.SetActive(false);
        GuideBook.SetActive(false);
        CardsMenu.SetActive(false);

        //open pausemenu UI
        PauseMenuUI.SetActive(true);
        //debug
        Debug.Log("Closed Sub Menu");
    }
    #endregion

    #endregion
}