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

    [HideInInspector]
    public PlayerController playerController;

    [Header("Main UI")]
    public GameObject ExplorationUI;
    public GameObject PauseMenuUI;
    public GameObject Player;
    public GameObject PlayerCamera;
    public Compass compass;
    public CompassNoIcon compass2;
    public Text HPTextCount;
    public Text PauseHPTextCount;
    public Slider MainHPBar;
    public Text goldCount;

    [Header("PauseMenu")]
    public GameObject SettingsMenu;
    public GameObject QuestMenuUI;
    public GameObject GuideBook;
    public GameObject CardsMenu;
    public GameObject WeaponsMenu;

    [Header("Confirm Screens")]
    public GameObject MainMenuConfirmScreen;
    public GameObject QuitConfirmScreen;

    [Header("Pause Menu Stats")]
    public Slider PauseHealthBar;
    public Slider PauseArcanaBar;
    public Text HPPotionCount;
    //public Text APPotionCount;
    //public Text RPotionCount;
    //public Text SPotionCount;

    public GenerateQuestInfo generateQuestInfo;

    #region DeckBuilder GameObjects
    [Header("DeckBuilder Menu")]
    public GameObject BaAtk;
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
    [Header("Help Guide Menu")]
    public bool activeSubPage;
    public bool firstSpreadPage;
    public bool lastSpreadPage;

    public GameObject ConstructsPage;
    public GameObject UndeadPage;
    public GameObject BeastsPage;
    public GameObject HumanoidPage;
    public GameObject SecretsPage;

    public GameObject HelpMainPage;
    public GameObject CompassIconPage;
    public GameObject EnemyCategory;
    public GameObject EnemyMainPage;
    public GameObject SpreadCardsHelp;
    public GameObject ReturnButton;

    [Header("Spread Cards")]
    public GameObject MainSpreadPage;
    public GameObject SpreadPage2;
    public GameObject SpreadPage3;
    public GameObject SpreadPage4;
    public GameObject SpreadPage5;
    public GameObject SpreadPage6;
    public GameObject spreadLastBtn;
    public GameObject spreadNextBtn;
    #endregion

    #endregion

    public void Setup()
    {
        //Pausemenu UI OFF
        PauseMenuUI.SetActive(false);
        //Exploration UI ON
        ExplorationUI.SetActive(true);
        //hide/lock mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    public void Update()
    {
        if (playerController != null)
        {
            //update the main HP Bars
            MainHPBar.value = playerController.health;
            MainHPBar.maxValue = playerController.maxHealth;
            HPTextCount.text = playerController.health.ToString();
            //update the inventory HP bars
            PauseHealthBar.value = playerController.health;
            PauseHealthBar.maxValue = playerController.maxHealth;
            PauseHPTextCount.text = playerController.health.ToString();
        }
        else
        {
            Debug.LogError("No player controller");

            playerController = GameObject.FindObjectOfType<PlayerController>();
        }

        if (playerController.canMove == true)
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
                if (generateQuestInfo != null)
                {
                    Debug.Log("Generated quest info");
                    generateQuestInfo.UpdateQuestInfo();
                }
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

            #region C open
            if (Input.GetKeyDown(KeyCode.C) && PauseMenuUI.activeSelf == false && QuestMenuUI.activeSelf == false)
            {
                CardsMenu.SetActive(true);
                ExplorationUI.SetActive(false);

                //freeze player/camera
                Player.GetComponent<PlayerController>().canMove = false;

                //unlock mouse - confined to window
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;

                //debug
                Debug.Log("Open Card menu");
            }
            #endregion
        }
        else if (playerController.canMove == false)
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

            #region C close
            if (Input.GetKeyDown(KeyCode.C)
                && PauseMenuUI.activeSelf == false
                && QuestMenuUI.activeSelf == false
                && CardsMenu.activeSelf == true)
            {
                //close card
                CardsMenu.SetActive(false);
                //open explore
                ExplorationUI.SetActive(true);

                //unfreeze player/camera
                Player.GetComponent<PlayerController>().canMove = true;

                //lock mouse - confined to window
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                //debug
                Debug.Log("Close card menu");
            }
            #endregion
        }

        #region Stats Update
        Debug.Log(PauseHealthBar.value + "||" + LoadSettings.instance.health);

        //update gold counter
        goldCount.text = LoadSettings.instance.currentGold.ToString();

        //update the number of all the potions
        HPPotionCount.text = LoadSettings.instance.healingPotionCount.ToString();
        //APPotionCount.text = loadSettings.arcanaPotionCount.ToString();
        //RPotionCount.text = loadSettings.potionCount.ToString();
        //SPotionCount.text = loadSettings.potionCount.ToString();
        #endregion

        #region CardsMenu Subpage button on/off
        if (CardsMenu.activeSelf == true && BaAtk.activeSelf == true)
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

        while (PauseMenuUI.activeSelf == false
            && QuestMenuUI.activeSelf == false
            && CardsMenu == false)
        {
            //unlock mouse - confined to window
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (activeSubPage == true)
        {
            ReturnButton.SetActive(true);
        }
        else
        {
            ReturnButton.SetActive(false);
        }

        if (HelpMainPage.activeSelf == true)
        {
            CompassIconPage.SetActive(false);
            EnemyCategory.SetActive(false);
            SpreadCardsHelp.SetActive(false);
            
            ConstructsPage.SetActive(false);
            UndeadPage.SetActive(false);
            BeastsPage.SetActive(false);
            HumanoidPage.SetActive(false);
            SecretsPage.SetActive(false);

            MainSpreadPage.SetActive(false);
            SpreadPage2.SetActive(false);
            SpreadPage3.SetActive(false);
            SpreadPage4.SetActive(false);
            SpreadPage5.SetActive(false);
            SpreadPage6.SetActive(false);
        }
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
        HelpMainPage.SetActive(true);
        activeSubPage = false;
        spreadLastBtn.SetActive(false);
        spreadNextBtn.SetActive(false);
    }

    public void CompassIcons()
    {
        CompassIconPage.SetActive(true);
        HelpMainPage.SetActive(false);
        activeSubPage = true;
    }

    public void Enemies()
    {
        EnemyCategory.SetActive(true);
        EnemyMainPage.SetActive(true);
        HelpMainPage.SetActive(false);
        activeSubPage = true;
    }

    public void SpreadCards()
    {
        SpreadCardsHelp.SetActive(true);
        MainSpreadPage.SetActive(true);
        HelpMainPage.SetActive(false);
        activeSubPage = true;
        spreadLastBtn.SetActive(false);
        spreadNextBtn.SetActive(true);
    }

    #region Enemy Buttons
    public void Constructs()
    {
        //1 list on all others off
        ConstructsPage.SetActive(true);
        EnemyMainPage.SetActive(false);
        activeSubPage = true;
    }

    public void Undead()
    {
        //1 list on all others off
        UndeadPage.SetActive(true);
        EnemyMainPage.SetActive(false);
        activeSubPage = true;
    }

    public void Beasts()
    {
        //1 list on all others off
        BeastsPage.SetActive(true);
        EnemyMainPage.SetActive(false);
        activeSubPage = true;
    }

    public void Humanoid()
    {
        //1 list on all others off
        HumanoidPage.SetActive(true);
        EnemyMainPage.SetActive(false);
        activeSubPage = true;
    }

    public void Secrets()
    {
        //1 list on all others off
        SecretsPage.SetActive(true);
        EnemyMainPage.SetActive(false);
        activeSubPage = true;
    }
    #endregion

    public void SpreadNext()
    {
        if (MainSpreadPage.activeSelf == true)
        {
            MainSpreadPage.SetActive(false);
            SpreadPage2.SetActive(true);
            spreadLastBtn.SetActive(true);
        }
        else if (SpreadPage2.activeSelf == true)
        {
            SpreadPage2.SetActive(false);
            SpreadPage3.SetActive(true);
            spreadLastBtn.SetActive(true);
        }
        else if (SpreadPage3.activeSelf == true)
        {
            SpreadPage3.SetActive(false);
            SpreadPage4.SetActive(true);
            spreadLastBtn.SetActive(true);
        }
        else if (SpreadPage4.activeSelf == true)
        {
            SpreadPage4.SetActive(false);
            SpreadPage5.SetActive(true);
            spreadLastBtn.SetActive(true);
        }
        else if (SpreadPage5.activeSelf == true)
        {
            SpreadPage5.SetActive(false);
            SpreadPage6.SetActive(true);
            spreadNextBtn.SetActive(false);
            spreadLastBtn.SetActive(true);
        }
    }

    public void SpreadLast()
    {
        if (SpreadPage6.activeSelf == true)
        {
            SpreadPage6.SetActive(false);
            SpreadPage5.SetActive(true);
            spreadNextBtn.SetActive(true);
        }
        else if (SpreadPage5.activeSelf == true)
        {
            SpreadPage5.SetActive(false);
            SpreadPage4.SetActive(true);
        }
        else if (SpreadPage4.activeSelf == true)
        {
            SpreadPage4.SetActive(false);
            SpreadPage3.SetActive(true);
        }
        else if (SpreadPage3.activeSelf == true)
        {
            SpreadPage3.SetActive(false);
            SpreadPage2.SetActive(true);
        }
        else if (SpreadPage2.activeSelf == true)
        {
            SpreadPage2.SetActive(false);
            MainSpreadPage.SetActive(true);
            spreadLastBtn.SetActive(false);
        }
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

    #region Card Menu Buttons
    public void DeckBuilder()
    {
        //turn off pause menu UI
        PauseMenuUI.SetActive(false);
        //turn on deck builder UI
        CardsMenu.SetActive(true);

        //BaAtk on
        BaAtk.SetActive(true);
        //MjArcCards off
        MjArcCardsPage.SetActive(false);
        //CorArcCards off

        //CardTypeName = basic atk Cards
        CardTypeTitle.text = "Basic Attack Cards";
        //Current Page = 1(x)
        PageX.text = "1";
        //Total Pages = 1(y)
        PageY.text = "1";

        //nextMjArc off
        NextMjArcBtn.SetActive(false);
        //lastMjArc off
        LastMjArcBtn.SetActive(false);
    }

    #region Submenu for cards
    public void BaAtkCards()
    {
        //BaAtkCards on
        BaAtk.SetActive(true);
        //MjArcCards off
        MjArcCardsPage.SetActive(false);
        //CorArcCards off

        //CardTypeName = Minor Arcana Cards
        CardTypeTitle.text = "Minor Arcana Cards";

        //Current Page = 1(x)
        PageX.text = "1";
        //Total Pages = 1(y)
        PageY.text = "1";

        //nextMjArc off
        NextMjArcBtn.SetActive(false);
        //lastMjArc off
        LastMjArcBtn.SetActive(false);
    }
    public void MjArcCards()
    {
        //BaAtkCards off
        BaAtk.SetActive(false);
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

        //nextMjArc on
        NextMjArcBtn.SetActive(true);
        //lastMjArc off
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
        //BaAtkCards off
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

    public void HelpReturn()
    {
        if (ConstructsPage.activeSelf == true || UndeadPage.activeSelf == true || BeastsPage.activeSelf == true ||
        HumanoidPage.activeSelf == true || SecretsPage.activeSelf == true)
        {
            EnemyMainPage.SetActive(true);

            ConstructsPage.SetActive(false);
            UndeadPage.SetActive(false);
            BeastsPage.SetActive(false);
            HumanoidPage.SetActive(false);
            SecretsPage.SetActive(false);
            activeSubPage = true;
        }
        else if (SpreadPage2.activeSelf == true || SpreadPage3.activeSelf == true || SpreadPage4.activeSelf == true ||
            SpreadPage5.activeSelf == true || SpreadPage6.activeSelf == true)
        {
            MainSpreadPage.SetActive(true);

            SpreadPage2.SetActive(false);
            SpreadPage3.SetActive(false);
            SpreadPage4.SetActive(false);
            SpreadPage5.SetActive(false);
            SpreadPage6.SetActive(false);
            spreadLastBtn.SetActive(false);
            spreadNextBtn.SetActive(true);
        }
        else
        {
            HelpMainPage.SetActive(true);

            CompassIconPage.SetActive(false);
            EnemyMainPage.SetActive(false);
            SpreadCardsHelp.SetActive(false);
            spreadLastBtn.SetActive(false);
            spreadNextBtn.SetActive(false);
            activeSubPage = false;
        }
    }
    #endregion

    #endregion
}