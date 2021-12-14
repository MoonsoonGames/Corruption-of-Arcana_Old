using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /*
     * COMP280 NOTE
     * 
     * This script was rewritten for comp280 worksheet 4 (Human-Computer interaction)
     * The script was originally built for handling the inventory and will now be used to handle all of the UI outside of combat 
     * All of the code has been rewritten to make sure it worked 100% for both GAM220 and COMP280
     */

    public GameObject Inventory;
    public GameObject ExplorationUI;
    public GameObject player;
    public GameObject Camera;
    public GameObject PlayingCanvas;

    public GameObject PauseMenu;
    public GameObject GuideBook;
    public GameObject SettingsPage;

    #region Main Menu
    public GameObject MenuCanvas;
    public GameObject MainMenuScreen;
    public GameObject MenuCredits;
    #endregion

    #region Inventory pages
    //Inventory pages
    [Header("Inventory")]
    public GameObject InvPage1;
    public GameObject InvPage2;
    public GameObject InvPage3;

    //inventory buttons
    [Header("Page Buttons")]
    public GameObject nextInvButton;
    public GameObject lastInvButton;
    #endregion

    #region Guide Book tabs
    //Guide book pages
    [Header("Guide book main pages")]
    public GameObject OpeningPage;
    public GameObject MonsterPages;
    public GameObject CardPages;
    public GameObject PeoplePages;
    public GameObject SecretsPages;
    public GameObject ControlsPages;
    public GameObject CombatPages;
    public GameObject ObjHistoryPages;

    //Guide book buttons
    [Header("Guidebook page buttons")]
    public GameObject nextGuideButton;
    public GameObject lastGuideButton;
    #endregion

    #region Right Tab pages
    #region Monster pages
    [Header("Guide book monster subpages")]
    public GameObject Monpage1;
    public GameObject Monpage2;
    #endregion

    #region Cards pages
    [Header("Guide book card subpages")]
    public GameObject Cardpage1;
    public GameObject Cardpage2;
    public GameObject Cardpage3;
    public GameObject Cardpage4;
    public GameObject Cardpage5;
    #endregion

    #region People pages
    [Header("Guide book people subpages")]
    public GameObject Pplpage1;
    public GameObject Pplpage2;
    #endregion

    #region Secret pages
    [Header("Guide book secret subpages")]
    public GameObject Secretpage1;
    #endregion
    #endregion

    #region Left Tab pages
    #region Control 
    [Header("Control section")]
    public GameObject ControlHome;
    public GameObject NavigationSubpage;
    public GameObject InventorySubpage;
    public GameObject PlayerStatSubpage;

    [Header("Navigation subpages")]
    public GameObject NavPage1;
    public GameObject NavPage2;
    public GameObject NavPage3;

    [Header("Inventory Help subpages")]
    public GameObject InvHelpPage1;
    public GameObject InvHelpPage2;
    public GameObject InvHelpPage3;

    [Header("Player Stat subpages")]
    public GameObject PlyrStatPage1;
    public GameObject PlyrStatPage2;
    #endregion

    #region Combat

    [Header("Combat Section")]
    public GameObject CombatHome;
    public GameObject CardsSubpage;
    public GameObject TargetSubpage;
    public GameObject TurnsSubpage;

    [Header("Cards subpages")]
    public GameObject CardsPage1;
    public GameObject CardsPage2;

    [Header("Targeting subpages")]
    public GameObject TargetPage1;
    public GameObject TargetPage2;

    [Header("Turns subpages")]
    public GameObject TurnsPage1;
    public GameObject TurnsPage2;
    #endregion
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Inventory.SetActive(false);
        ExplorationUI.SetActive(true);
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {      
        #region Hotkeys
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Inventory.activeSelf == true)
            {
                PauseMenu.SetActive(false);
            }
            else if (GuideBook.activeSelf == true)
            {
                PauseMenu.SetActive(false);
            }
            else if (SettingsPage.activeSelf == true)
            {
                PauseMenu.SetActive(false);
            }
            else if (Inventory.activeSelf == false)
            {
                PauseMenu.SetActive(true);
                ExplorationUI.SetActive(false);
                player.GetComponent<PlayerController>().enabled = false;
                Camera.GetComponent<PlayerCameraController>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (PauseMenu.activeSelf == true)
            {
                Inventory.SetActive(false);
            }
            else if (GuideBook.activeSelf == true)
            {
                Inventory.SetActive(false);
            }
            else if (SettingsPage.activeSelf == true)
            {
                Inventory.SetActive(false);
            }
            else if (PauseMenu.activeSelf == false)
            {
                Inventory.SetActive(true);
                ExplorationUI.SetActive(false);
                player.GetComponent<PlayerController>().enabled = false;
                Camera.GetComponent<PlayerCameraController>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            if (PauseMenu.activeSelf == true)
            {
                GuideBook.SetActive(false);
            }
            else if (GuideBook.activeSelf == true)
            {
                GuideBook.SetActive(false);
            }
            else if (SettingsPage.activeSelf == true)
            {
                GuideBook.SetActive(false);
            }
            else if (Inventory.activeSelf == true)
            {
                GuideBook.SetActive(false);
            }
            else if (GuideBook.activeSelf == false)
            {
                GuideBook.SetActive(true);
                ExplorationUI.SetActive(false);
                player.GetComponent<PlayerController>().enabled = false;
                Camera.GetComponent<PlayerCameraController>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        //else if (ActiveScene == "Thoth" && Input.GetKeyDown(KeyCode.M))
        //{
        //    ThothMap.SetActive(true);
        //    ExplorationUI.SetActive(false);
        //    player.GetComponent<PlayerController>().enabled = false;
        //    Camera.GetComponent<PlayerCameraController>().enabled = false;
        //}
        //else if (ActiveScene == "Clearing" && Input.GetKeyDown(KeyCode.M))
        //{
        //    ClearingMap.SetActive(true);
        //    ExplorationUI.SetActive(false);
        //    player.GetComponent<PlayerController>().enabled = false;
        //    Camera.GetComponent<PlayerCameraController>().enabled = false;
        //}
        #endregion

        #region Guide book buttons
        if (OpeningPage.activeSelf == true)
        {
            nextGuideButton.SetActive(false);
            lastGuideButton.SetActive(false);
        }

        //Monster pages
        else if (MonsterPages.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (Monpage1.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(false);
            }
            else if  (Monpage2.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(true);
            }
        }

        //Card pages
        else if (CardPages.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (Cardpage1.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(false);
            }
            else if (Cardpage2.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(true);
            }
            else if (Cardpage3.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(true);
            }
            else if (Cardpage4.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(true);
            }
            else if (Cardpage5.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(true);
            }
        }

        //People pages
        else if (PeoplePages.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (Pplpage1.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(false);
            }
            else if  (Pplpage2.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(true);
            }
        }

        //Secret pages
        else if (SecretsPages.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (Secretpage1.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(false);
            }
        }

        //Navigation pages
        else if (NavigationSubpage.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (NavPage1.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(false);
            }
            else if (NavPage2.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(true);
            }
            else if (NavPage3.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(true);
            }
        }

        //InventoryHelp pages
        else if (InventorySubpage.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (InvHelpPage1.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(false);
            }
            else if (InvHelpPage2.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(true);
            }
            else if (InvHelpPage3.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(true);
            }
        }

        //Player Stats pages
        else if (PlayerStatSubpage.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (PlyrStatPage1.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(false);
            }
            else if  (PlyrStatPage2.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(true);
            }
        }

        //Cards Help pages
        else if (CardsSubpage.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (CardsPage1.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(false);
            }
            else if  (CardsPage2.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(true);
            }
        }

        //Targeting pages
        else if (TargetSubpage.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (TargetPage1.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(false);
            }
            else if (TargetPage2.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(true);
            }
        }

        //Turns pages
        else if (TurnsSubpage.activeSelf == true)
        {
            nextGuideButton.SetActive(true);
            lastGuideButton.SetActive(false);
            if (TurnsPage1.activeSelf == true)
            {
                nextGuideButton.SetActive(true);
                lastGuideButton.SetActive(false);
            }
            else if (TurnsPage2.activeSelf == true)
            {
                nextGuideButton.SetActive(false);
                lastGuideButton.SetActive(true);
            }
        }
        #endregion
    }

    #region Pause menu buttons 
    public void Resume()
    {
        PauseMenu.SetActive(false);
        ExplorationUI.SetActive(true);

        player.GetComponent<PlayerController>().enabled = true;
        Camera.GetComponent<PlayerCameraController>().enabled = true;
        Cursor.visible = false;

        Debug.Log("Resume Playing");
    }

    public void Settings()
    {
        PauseMenu.SetActive(false);
        SettingsPage.SetActive(true);

        Debug.Log("changing Settings");
    }

    public void Help()
    {
        PauseMenu.SetActive(false);
        GuideBook.SetActive(true);

        OpeningPage.SetActive(true);
        MonsterPages.SetActive(false);
        CardPages.SetActive(false);
        PeoplePages.SetActive(false);
        SecretsPages.SetActive(false);
        ControlsPages.SetActive(false);
        CombatPages.SetActive(false);
        ObjHistoryPages.SetActive(false);

        Debug.Log("Loading Help");
    }

    public void MainMenu()
    {
        PlayingCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
        Camera.GetComponent<PlayerCameraController>().enabled = false;
        Debug.Log("Back to the start");
    }

    public void CloseGame()
    {
        Application.Quit();

        Debug.Log("Quiting game");
    }

    public void resumePlaying()
    {
        PlayingCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
        player.GetComponent<PlayerController>().enabled = false;
        Camera.GetComponent<PlayerCameraController>().enabled = false;
        Debug.Log("Back to the game");
    }
    #endregion

    #region Inventory Buttons
    public void CloseMenu()
    {
        Inventory.SetActive(false);
        ExplorationUI.SetActive(true);
        Cursor.visible = false;
        player.GetComponent<PlayerController>().enabled = true;
        Camera.GetComponent<PlayerCameraController>().enabled = true;
        Debug.Log("Closed Menu");
    }

    public void NextInvPage()
    {
        if (InvPage1.activeSelf == true)
        {
            InvPage1.SetActive(false);
            InvPage2.SetActive(true);
            nextInvButton.SetActive(true);
            lastInvButton.SetActive(true);
        }
        else if (InvPage2.activeSelf == true)
        {
            InvPage2.SetActive(false);
            InvPage3.SetActive(true);
            nextInvButton.SetActive(false);
            lastInvButton.SetActive(true);
        }
    }

    public void LastInvPage()
    {
        if (InvPage3.activeSelf == true)
        {
            InvPage3.SetActive(false);
            InvPage2.SetActive(true);
            nextInvButton.SetActive(true);
            lastInvButton.SetActive(true);
        }
        else if (InvPage2.activeSelf == true)
        {
            InvPage2.SetActive(false);
            InvPage1.SetActive(true);
            nextInvButton.SetActive(true);
            lastInvButton.SetActive(false);
        }
    }

    #endregion

    #region Guide book - right tabs(stats/lore)

    public void Monsters()
    {
        //Main pages (parents)
        OpeningPage.SetActive(false);
        MonsterPages.SetActive(true);
        CardPages.SetActive(false);
        PeoplePages.SetActive(false);
        SecretsPages.SetActive(false);
        ControlsPages.SetActive(false);
        CombatPages.SetActive(false);
        ObjHistoryPages.SetActive(false);

        //Sub pages (child)
        Monpage1.SetActive(true);
        Monpage2.SetActive(false);

        //buttons
        nextGuideButton.SetActive(true);
        lastGuideButton.SetActive(false);
    }

    public void Cards()
    {
        //Main pages (parents)
        OpeningPage.SetActive(false);
        MonsterPages.SetActive(false);
        CardPages.SetActive(true);
        PeoplePages.SetActive(false);
        SecretsPages.SetActive(false);
        ControlsPages.SetActive(false);
        CombatPages.SetActive(false);
        ObjHistoryPages.SetActive(false);

        //Sub pages (child)
        Cardpage1.SetActive(true);
        Cardpage2.SetActive(false);
        Cardpage3.SetActive(false);
        Cardpage4.SetActive(false);
        Cardpage5.SetActive(false);

        //buttons
        nextGuideButton.SetActive(true);
        lastGuideButton.SetActive(false);
    }

    public void People()
    {
        //Main pages (parents)
        OpeningPage.SetActive(false);
        MonsterPages.SetActive(false);
        CardPages.SetActive(false);
        PeoplePages.SetActive(true);
        SecretsPages.SetActive(false);
        ControlsPages.SetActive(false);
        CombatPages.SetActive(false);
        ObjHistoryPages.SetActive(false);

        //Sub pages (child)
        Pplpage1.SetActive(true);
        Pplpage2.SetActive(false);

        //buttons
        nextGuideButton.SetActive(true);
        lastGuideButton.SetActive(false);
    }

    public void Secrets()
    {
        //Main pages (parents)
        OpeningPage.SetActive(false);
        MonsterPages.SetActive(false);
        CardPages.SetActive(false);
        PeoplePages.SetActive(false);
        SecretsPages.SetActive(true);
        ControlsPages.SetActive(false);
        CombatPages.SetActive(false);
        ObjHistoryPages.SetActive(false);

        //Sub pages (child)
        Secretpage1.SetActive(true);

        //buttons
        nextGuideButton.SetActive(false);
        lastGuideButton.SetActive(false);
    }
    #endregion

    #region Guide book - left Tabs(help)
        #region controls
    public void Controls()
    {
        //Main pages (parents)
        OpeningPage.SetActive(false);
        MonsterPages.SetActive(false);
        CardPages.SetActive(false);
        PeoplePages.SetActive(false);
        SecretsPages.SetActive(false);
        ControlsPages.SetActive(true);
        CombatPages.SetActive(false);
        ObjHistoryPages.SetActive(false);

        //Sub pages (child)
        ControlHome.SetActive(true);
        NavigationSubpage.SetActive(false);
        InventorySubpage.SetActive(false);
        PlayerStatSubpage.SetActive(false);

        //buttons
        nextGuideButton.SetActive(false);
        lastGuideButton.SetActive(false);
    }

    public void NavigationHelp()
    {
        //main subpages
        ControlHome.SetActive(false);
        NavigationSubpage.SetActive(true);
        InventorySubpage.SetActive(false);
        PlayerStatSubpage.SetActive(false);

        //child subpages
        NavPage1.SetActive(true);
        NavPage2.SetActive(false);
        NavPage3.SetActive(false);

        //buttons
        nextGuideButton.SetActive(true);
        lastGuideButton.SetActive(false);
    }

    public void InventoryHelp()
    {
        //main subpages
        ControlHome.SetActive(false);
        NavigationSubpage.SetActive(false);
        InventorySubpage.SetActive(true);
        PlayerStatSubpage.SetActive(false);

        //child subpages
        InvHelpPage1.SetActive(true);
        InvHelpPage2.SetActive(false);
        InvHelpPage3.SetActive(false);

        //buttons
        nextGuideButton.SetActive(true);
        lastGuideButton.SetActive(false);
    }

    public void PlayerStats()
    {
        //main subpages
        ControlHome.SetActive(false);
        NavigationSubpage.SetActive(false);
        InventorySubpage.SetActive(false);
        PlayerStatSubpage.SetActive(true);

        //child subpages
        PlyrStatPage1.SetActive(true);
        PlyrStatPage2.SetActive(false);

        //buttons
        nextGuideButton.SetActive(true);
        lastGuideButton.SetActive(false);
    }
        #endregion

        #region combat
    public void Combat()
    {
        //Main pages (parents)
        OpeningPage.SetActive(false);
        MonsterPages.SetActive(false);
        CardPages.SetActive(false);
        PeoplePages.SetActive(false);
        SecretsPages.SetActive(false);
        ControlsPages.SetActive(false);
        CombatPages.SetActive(true);
        ObjHistoryPages.SetActive(false);

        //Sub pages (child)
        CombatHome.SetActive(true);
        CardsSubpage.SetActive(false);
        TargetSubpage.SetActive(false);
        TurnsSubpage.SetActive(false);

        //buttons
        nextGuideButton.SetActive(false);
        lastGuideButton.SetActive(false);
    }

    public void CardsHelp()
    {
        //main subpages
        CombatHome.SetActive(false);
        CardsSubpage.SetActive(true);
        TargetSubpage.SetActive(false);
        TurnsSubpage.SetActive(false);

        //child subpages
        CardsPage1.SetActive(true);
        CardsPage2.SetActive(false);

        //buttons
        nextGuideButton.SetActive(true);
        lastGuideButton.SetActive(false);
    }

    public void TargetHelp()
    {
        //main subpages
        CombatHome.SetActive(false);
        CardsSubpage.SetActive(false);
        TargetSubpage.SetActive(true);
        TurnsSubpage.SetActive(false);

        //child subpages
        TargetPage1.SetActive(true);
        TargetPage2.SetActive(false);

        //buttons
        nextGuideButton.SetActive(true);
        lastGuideButton.SetActive(false);
    }

    public void TurnsHelp()
    {
        //main subpages
        CombatHome.SetActive(false);
        CardsSubpage.SetActive(false);
        TargetSubpage.SetActive(false);
        TurnsSubpage.SetActive(true);

        //child subpages
        TurnsPage1.SetActive(true);
        TurnsPage2.SetActive(false);

        //buttons
        nextGuideButton.SetActive(true);
        lastGuideButton.SetActive(false);
    }
    #endregion

        #region objective history
    public void ObjectiveHistory()
    {
        OpeningPage.SetActive(false);
        Monpage1.SetActive(false);
        Cardpage1.SetActive(false);
        Pplpage1.SetActive(false);
        Secretpage1.SetActive(false);
        ControlsPages.SetActive(false);
        CombatPages.SetActive(false);
        ObjHistoryPages.SetActive(true);

        //buttons
        nextGuideButton.SetActive(false);
        lastGuideButton.SetActive(false);
    }
    #endregion
    #endregion

    #region Page buttons (next/last/close)
    public void nextPage()
    {
        //monster section
        if (Monpage1.activeSelf == true)
        {
            Monpage1.SetActive(false);
            Monpage2.SetActive(true);
        }

        //card section
        if (Cardpage1.activeSelf == true)
        {
            Cardpage1.SetActive(false);
            Cardpage2.SetActive(true);
        }
        else if (Cardpage2.activeSelf == true)
        {
            Cardpage2.SetActive(false);
            Cardpage3.SetActive(true);
        }
        else if (Cardpage3.activeSelf == true)
        {
            Cardpage3.SetActive(false);
            Cardpage4.SetActive(true);
        }
        else if (Cardpage4.activeSelf == true)
        {
            Cardpage4.SetActive(false);
            Cardpage5.SetActive(true);
        }

        //people section
        if (Pplpage1.activeSelf == true)
        {
            Pplpage1.SetActive(false);
            Pplpage2.SetActive(true);
        }

        //Control Section
        //Nav section
        if (NavPage1.activeSelf == true)
        {
            NavPage1.SetActive(false);
            NavPage2.SetActive(true);
        }
        else if (NavPage2.activeSelf == true)
        {
            NavPage2.SetActive(false);
            NavPage3.SetActive(true);
        }

        //inventory help section
        if (InvHelpPage1.activeSelf == true)
        {
            InvHelpPage1.SetActive(false);
            InvHelpPage2.SetActive(true);
        }
        else if (InvHelpPage2.activeSelf == true)
        {
            InvHelpPage2.SetActive(false);
            InvHelpPage3.SetActive(true);
        }

        //Player Stats section
        if (PlyrStatPage1.activeSelf == true)
        {
            PlyrStatPage1.SetActive(false);
            PlyrStatPage2.SetActive(true);
        }

        //Combat Section
        //Cards Section
        if (CardsPage1.activeSelf == true)
        {
            CardsPage1.SetActive(false);
            CardsPage2.SetActive(true);
        }

        //Target Section
        if (TargetPage1.activeSelf == true)
        {
            TargetPage1.SetActive(false);
            TargetPage2.SetActive(true);
        }

        //Turns Section
        if (TurnsPage1.activeSelf == true)
        {
            TurnsPage1.SetActive(false);
            TurnsPage2.SetActive(true);
        }
    }

    public void lastPage()
    {
        //monster section
        if (Monpage2.activeSelf == true)
        {
            Monpage2.SetActive(false);
            Monpage1.SetActive(true);
        }

        //card section
        if (Cardpage5.activeSelf == true)
        {
            Cardpage5.SetActive(false);
            Cardpage4.SetActive(true);
        }
        else if (Cardpage4.activeSelf == true)
        {
            Cardpage4.SetActive(false);
            Cardpage3.SetActive(true);
        }
        else if (Cardpage3.activeSelf == true)
        {
            Cardpage3.SetActive(false);
            Cardpage2.SetActive(true);
        }
        else if (Cardpage2.activeSelf == true)
        {
            Cardpage2.SetActive(false);
            Cardpage1.SetActive(true);
        }

        //people section
        if (Pplpage2.activeSelf == true)
        {
            Pplpage2.SetActive(false);
            Pplpage1.SetActive(true);
        }

        //Control Section
        //Nav section
        if (NavPage3.activeSelf == true)
        {
            NavPage3.SetActive(false);
            NavPage2.SetActive(true);
        }
        else if (NavPage2.activeSelf == true)
        {
            NavPage2.SetActive(false);
            NavPage1.SetActive(true);
        }

        //inventory help section
        if (InvHelpPage3.activeSelf == true)
        {
            InvHelpPage3.SetActive(false);
            InvHelpPage2.SetActive(true);
        }
        else if (InvHelpPage2.activeSelf == true)
        {
            InvHelpPage2.SetActive(false);
            InvHelpPage1.SetActive(true);
        }

        //Player Stats section
        if (PlyrStatPage2.activeSelf == true)
        {
            PlyrStatPage2.SetActive(false);
            PlyrStatPage1.SetActive(true);
        }

        //Combat Section
        //Cards Section
        if (CardsPage2.activeSelf == true)
        {
            CardsPage2.SetActive(false);
            CardsPage1.SetActive(true);
        }

        //Target Section
        if (TargetPage2.activeSelf == true)
        {
            TargetPage2.SetActive(false);
            TargetPage1.SetActive(true);
        }

        //Turns Section
        if (TurnsPage2.activeSelf == true)
        {
            TurnsPage2.SetActive(false);
            TurnsPage1.SetActive(true);
        }
    }

    public void CloseBook()
    {
        GuideBook.SetActive(false);
        PauseMenu.SetActive(true);
    }
    
    public void CloseSettings()
    {
        SettingsPage.SetActive(false);
        PauseMenu.SetActive(true);
    }
    #endregion
}