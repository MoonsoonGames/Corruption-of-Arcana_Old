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
    public CompassNoIcon compass2;

    public GameObject SettingsMenu;
    public GameObject QuestMenuUI;
    public GameObject GuideBook;
    public GameObject CardsMenu;
    public GameObject WeaponsMenu;

    public GameObject MainMenuConfirmScreen;
    public GameObject QuitConfirmScreen;

    public Slider PauseHealthBar;
    public Slider PauseArcanaBar;
    public Text HPPotionCount;
    //public Text APPotionCount;
    //public Text RPotionCount;
    //public Text SPotionCount;

    public Text HPTextCount;
    public Text PauseHPTextCount;
    public Slider MainHPBar;

    public Text goldCount;

    #region DeckBuilder GameObjects
    public GameObject MiArc;
    public GameObject MjArcCardsPage;
    public GameObject MjArc1;
    public GameObject MjArc2;
    public GameObject MjArc3;
    //public GameObject CorArcCardsPage;
    public GameObject NextMjArcBtn;
    public GameObject LastMjArcBtn;

    public Text CardTypeTitle;
    public Text PageX;
    public Text PageY;
    #endregion

    #region HelpGuide
    public GameObject ConstructsList;
    public GameObject UndeadList;
    public GameObject BeastsList;
    public GameObject HumanoidList;
    public GameObject SecretsList;

    public Text SpeciesText;
    #endregion

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
        //update the main HP Bars
        MainHPBar.value = loadSettings.health;
        MainHPBar.maxValue = loadSettings.maxHealth;
        HPTextCount.text = loadSettings.health.ToString();
        //update the inventory HP bars
        PauseHealthBar.value = loadSettings.health;
        PauseHealthBar.maxValue = loadSettings.maxHealth;
        PauseHPTextCount.text = loadSettings.health.ToString();

        if (Player.GetComponent<PlayerController>().canMove == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

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
        else if (Player.GetComponent<PlayerController>().canMove == false)
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

            #region J close
            if (Input.GetKeyDown(KeyCode.J) 
                && PauseMenuUI.activeSelf == false 
                && QuestMenuUI.activeSelf == true)
            {
                //close quest
                QuestMenuUI.SetActive(false);
                //open explore
                ExplorationUI.SetActive(true);

                //unfreeze player/camera
                Player.GetComponent<PlayerController>().canMove = true;

                //lock mouse - confined to window
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                //debug
                Debug.Log("Close Quest menu");
            }
            #endregion
        }

        #region Stats Update
        Debug.Log(PauseHealthBar.value + "||" + loadSettings.health);

        //update gold counter
        goldCount.text = loadSettings.currentGold.ToString();

        //update the number of all the potions
        HPPotionCount.text = loadSettings.healingPotionCount.ToString();
        //APPotionCount.text = loadSettings.arcanaPotionCount.ToString();
        //RPotionCount.text = loadSettings.potionCount.ToString();
        //SPotionCount.text = loadSettings.potionCount.ToString();
        #endregion

        #region CardsMenu Subpage button on/off
        if (CardsMenu.activeSelf == true && MiArc.activeSelf == true)
        {
            NextMjArcBtn.SetActive(false);
            LastMjArcBtn.SetActive(false);
        }

        if (CardsMenu.activeSelf == true && MjArc1.activeSelf == true)
        {
            NextMjArcBtn.SetActive(true);
            LastMjArcBtn.SetActive(false);
        }
        else if (CardsMenu.activeSelf == true && MjArc2.activeSelf == true)
        {
            NextMjArcBtn.SetActive(true);
            LastMjArcBtn.SetActive(true);
        }
        else if (CardsMenu.activeSelf == true && MjArc3.activeSelf == true)
        {
            NextMjArcBtn.SetActive(false);
            LastMjArcBtn.SetActive(true);
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
        GuideBook.SetActive(true);
    }

    #region HelpGuide Buttons
    public void Constructs()
    {
        //1 list on all others off
        ConstructsList.SetActive(true);
        UndeadList.SetActive(false);
        BeastsList.SetActive(false);
        HumanoidList.SetActive(false);
        SecretsList.SetActive(false);

        SpeciesText.text = "Constructs";
    }

    public void Undead()
    {
        //1 list on all others off
        ConstructsList.SetActive(false);
        UndeadList.SetActive(true);
        BeastsList.SetActive(false);
        HumanoidList.SetActive(false);
        SecretsList.SetActive(false);

        SpeciesText.text = "Undead";
    }

    public void Beasts()
    {
        //1 list on all others off
        ConstructsList.SetActive(false);
        UndeadList.SetActive(false);
        BeastsList.SetActive(true);
        HumanoidList.SetActive(false);
        SecretsList.SetActive(false);

        SpeciesText.text = "Beasts";
    }

    public void Humanoid()
    {
        //1 list on all others off
        ConstructsList.SetActive(false);
        UndeadList.SetActive(false);
        BeastsList.SetActive(false);
        HumanoidList.SetActive(true);
        SecretsList.SetActive(false);

        SpeciesText.text = "Humanoids";
    }

    public void Secrets()
    {
        //1 list on all others off
        ConstructsList.SetActive(false);
        UndeadList.SetActive(false);
        BeastsList.SetActive(false);
        HumanoidList.SetActive(false);
        SecretsList.SetActive(true);

        SpeciesText.text = "Secrets";
    }
    #endregion

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

    #region Card Menu Buttons
    public void DeckBuilder()
    {
        //turn off pause menu UI
        PauseMenuUI.SetActive(false);
        //turn on deck builder UI
        CardsMenu.SetActive(true);

        //MiArcCards on
        MiArc.SetActive(true);
        //MjArcCards off
        MjArcCardsPage.SetActive(false);
        //CorArcCards off

        //CardTypeName = Minor Arcana Cards
        CardTypeTitle.text = "Minor Arcana Cards";
        //Current Page = 1(x)
        PageX.text = "1";
        //Total Pages = 1(y)
        PageY.text = "1";

        //nextMiArc off
        NextMjArcBtn.SetActive(false);
        //lastMiArc off
        LastMjArcBtn.SetActive(false);
    }

    #region Submenu for cards
    public void MiArcCards()
    {
        //MiArcCards on
        MiArc.SetActive(true);
        //MjArcCards off
        MjArcCardsPage.SetActive(false);
        //CorArcCards off

        //CardTypeName = Minor Arcana Cards
        CardTypeTitle.text = "Minor Arcana Cards";

        //Current Page = 1(x)
        PageX.text = "1";
        //Total Pages = 1(y)
        PageY.text = "1";

        //nextMiArc off
        NextMjArcBtn.SetActive(false);
        //lastMiArc off
        LastMjArcBtn.SetActive(false);
    }
    public void MjArcCards()
    {
        //MiArcCards off
        MiArc.SetActive(false);
        //MjArcCards on
        MjArcCardsPage.SetActive(true);
        //CorArcCards off

        //MjArc1 on
        MjArc1.SetActive(true);
        //MjArc2/3 off
        MjArc2.SetActive(false);
        MjArc3.SetActive(false);

        //CardTypeName = Major Arcana Cards
        CardTypeTitle.text = "Major Arcana Cards";
        //Current Page = 1(x)
        PageX.text = "1";
        //Total Pages = 3(y)
        PageY.text = "3";

        //nextMiArc on
        NextMjArcBtn.SetActive(true);
        //lastMiArc off
        LastMjArcBtn.SetActive(false);
    }

    #region MjArc Page buttons
    public void NextMjArc()
    {
        if (MjArc1.activeSelf == true)
        {
            MjArc1.SetActive(false);
            MjArc2.SetActive(true);

            PageX.text = "2";
            PageY.text = "3";
        }
        else if (MjArc2.activeSelf == true)
        {
            MjArc2.SetActive(false);
            MjArc3.SetActive(true);

            PageX.text = "3";
            PageY.text = "3";
        }
    }
    public void LastMjArc()
    {
        if (MjArc3.activeSelf == true)
        {
            MjArc3.SetActive(false);
            MjArc2.SetActive(true);

            //Current Page = 2(x)
            PageX.text = "2";
            //Total Pages = 3(y)
            PageY.text = "3";
        }
        else if (MjArc2.activeSelf == true)
        {
            MjArc2.SetActive(false);
            MjArc1.SetActive(true);

            //Current Page = 1(x)
            PageX.text = "1";
            //Total Pages = 3(y)
            PageY.text = "3";
        }
    }
    #endregion

    public void CorArcCards()
    {
        //MiArcCards off
        //MjArcCards off
        //CorArcCards on

        //CardTypeName = Corrupted Arcana Cards
        //Current Page = 1(x)
        //Total Pages = 1(y)

        //nextMjArc off
        //lastMjArc off
    }

    public void Weapons()
    {
        WeaponsMenu.SetActive(true);
        CardsMenu.SetActive(false);
    }

    public void CloseWeapons()
    {
        WeaponsMenu.SetActive(false);
        CardsMenu.SetActive(true);
    }
    #endregion

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