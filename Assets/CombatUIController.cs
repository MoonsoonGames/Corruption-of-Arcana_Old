using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIController : MonoBehaviour
{
    LoadSettings loadSettings;
    public GameObject MainCombatUI;
    public GameObject PauseMenu;
    public GameObject HelpMenu;
    public GameObject CardsMenu;
    public GameObject SettingsMenu;
    public GameObject QuitConfirm;

    public GameObject PotionOpenBtn;
    public GameObject PotionCloseBtn;
    public GameObject PotionBar;

    public bool Fighting;

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

    public Slider HPBar;

    // Start is called before the first frame update
    void Start()
    {
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
                HelpMenu.SetActive(false);
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
                    HelpMenu.SetActive(false);
                    SettingsMenu.SetActive(false);
                    QuitConfirm.SetActive(false);

                    Fighting = true;
                }
            }
            #endregion
        }

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
    #endregion

    public void Help()
    {
        PauseMenu.SetActive(false);
        HelpMenu.SetActive(true);
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

    public void Settings()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void PotionOpen()
    {
        PotionBar.SetActive(true);
        PotionCloseBtn.SetActive(true);

        PotionOpenBtn.SetActive(false);
    }

    public void PotionClose()
    {
        PotionBar.SetActive(false);
        PotionCloseBtn.SetActive(false);

        PotionOpenBtn.SetActive(true);
    }

    public void CloseSubMenu()
    {
        //close settings/guidebook menus
        SettingsMenu.SetActive(false);
        HelpMenu.SetActive(false);
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