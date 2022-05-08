using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIController : MonoBehaviour
{
    LoadSettings loadSettings;
    AbilityManager abilityManager;
    public GameObject MainCombatUI;
    public GameObject PauseMenu;
    public GameObject GuideBook;
    public GameObject CardsMenu;
    public GameObject SettingsMenu;
    public GameObject QuitConfirm;

    public GameObject PotionOpenBtn;
    public GameObject PotionCloseBtn;
    public GameObject PotionBar;

    public bool Fighting;

    #region DeckBuilder GameObjects
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

    public Slider HPBar;

    // Start is called before the first frame update
    void Start()
    {
        loadSettings = LoadSettings.instance;
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
        MainCombatUI.SetActive(true);
        PauseMenu.SetActive(false);
        CardsMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        QuitConfirm.SetActive(false);

        PotionOpenBtn.SetActive(true);
        PotionCloseBtn.SetActive(false);
        PotionBar.SetActive(false);
        Fighting = true;
        HPBar.maxValue = loadSettings.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Fighting == true)
        {
            #region Combat Pause open
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MainCombatUI.SetActive(false);
                PauseMenu.SetActive(true);
                CardsMenu.SetActive(false);
                GuideBook.SetActive(false);
                SettingsMenu.SetActive(false);
                QuitConfirm.SetActive(false);

                Fighting = false;
            }
            #endregion
        }

        else if (Fighting == false)
        {
            #region Combat Pause close
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PauseMenu.activeSelf == true)
                {
                    MainCombatUI.SetActive(true);
                    PauseMenu.SetActive(false);
                    CardsMenu.SetActive(false);
                    GuideBook.SetActive(false);
                    SettingsMenu.SetActive(false);
                    QuitConfirm.SetActive(false);

                    Fighting = true;
                }
            }
            #endregion
        }

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

        #region Guide Book
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
        #endregion
}

    public void PauseButton()
    {
        PauseMenu.SetActive(true);
        MainCombatUI.SetActive(false);
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        MainCombatUI.SetActive(true);
    }

    public void DeckBuilder()
    {
        //turn off pause menu UI
        PauseMenu.SetActive(false);
        //turn on deck builder UI
        CardsMenu.SetActive(true);

        //MiArcCards on
        BaAtk.SetActive(true);
        //MjArcCards off
        MjArcCardsPage.SetActive(false);
        //CorArcCards off

        //CardTypeName = Minor Arcana Cards
        CardTypeTitle.text = "Basic Attack Cards";
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
        BaAtk.SetActive(true);
        //MjArcCards off
        MjArcCardsPage.SetActive(false);
        //CorArcCards off

        //CardTypeName = Minor Arcana Cards
        CardTypeTitle.text = "Basic Attack Cards";

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
    #endregion


    #region Help Button
    public void Help()
    {
        //turn off pause menu UI
        PauseMenu.SetActive(false);
        //turn on GuideBook UI
        GuideBook.SetActive(true);
        HelpMainPage.SetActive(true);
        activeSubPage = false;
        spreadLastBtn.SetActive(false);
        spreadNextBtn.SetActive(false);
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

    public void Settings()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void PotionOpen()
    {
        PotionBar.SetActive(true);
        PotionCloseBtn.SetActive(true);

        abilityManager.endTurn.OpenMenu(false);

        PotionOpenBtn.SetActive(false);

        abilityManager.ResetAbility();
    }

    public void PotionClose()
    {
        PotionBar.SetActive(false);
        PotionCloseBtn.SetActive(false);

        PotionOpenBtn.SetActive(true);

        abilityManager.ResetAbility();
    }

    public void CloseSubMenu()
    {
        //close settings/guidebook menus
        SettingsMenu.SetActive(false);
        GuideBook.SetActive(false);
        CardsMenu.SetActive(false);

        //open pausemenu UI
        PauseMenu.SetActive(true);
        //debug
        Debug.Log("Closed Sub Menu");
    }

    #region Quit Stuff
    public void QuitScreen()
    {
        PauseMenu.SetActive(false);
        QuitConfirm.SetActive(true);
    }
    public void QuitConfirmY()
    {
        Application.Quit();
    }

    public void QuitConfirmN()
    {
        QuitConfirm.SetActive(false);
        PauseMenu.SetActive(true);
    }
    #endregion
}