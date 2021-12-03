using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /*
 * COMP280 NOTE
 * 
 * This script was rewritten for comp280 worksheet 4 (Human-Computer interaction)
 * The script was originally built for handling the inventory and will now be used to handle all of the UI outside of combat 
 */

    public GameObject Inventory;
    public GameObject ExplorationUI;
    public GameObject player;
    public GameObject Camera;

    public GameObject PauseMenu;
    public GameObject GuideBook;

    //Inventory pages
    public GameObject InvPage1;
    public GameObject InvPage2;
    public GameObject InvPage3;

    //Guide book pages
    public GameObject OpeningPage;
    public GameObject MonsterPages;
    public GameObject CardPages;
    public GameObject PeoplePages;
    public GameObject SecretsPages;
    public GameObject ControlsPages;
    public GameObject CombatPages;
    public GameObject ObjHistoryPages;

    public GameObject Monpage1;
    public GameObject Monpage2;

    public GameObject Cardpage1;
    public GameObject Cardpage2;
    public GameObject Cardpage3;
    public GameObject Cardpage4;
    public GameObject Cardpage5;

    public GameObject Pplpage1;
    public GameObject Pplpage2;

    public GameObject Secretpage1;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.SetActive(false);
        ExplorationUI.SetActive(true);
        PauseMenu.SetActive(false);

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Inventory.activeSelf == true)
            {
                PauseMenu.SetActive(false);
                Cursor.visible = false;
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
                Cursor.visible = false;
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

    public void Help()
    {
        PauseMenu.SetActive(false);
        GuideBook.SetActive(true);

        Debug.Log("Loading Help");
    }

    public void MainMenu()
    {
        Debug.Log("Back to the start");
    }

    public void CloseGame()
    {
        Application.Quit();

        Debug.Log("Quiting game");
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
        }
        else if (InvPage2.activeSelf == true)
        {
            InvPage2.SetActive(false);
            InvPage3.SetActive(true);
        }
    }

    public void LastInvPage()
    {
        if (InvPage3.activeSelf == true)
        {
            InvPage3.SetActive(false);
            InvPage2.SetActive(true);
        }
        else if (InvPage2.activeSelf == true)
        {
            InvPage2.SetActive(false);
            InvPage1.SetActive(true);
        }
    }

    #endregion

    #region Guide book (help)

    public void Monsters()
    {
        MonsterPages.SetActive(true);
        OpeningPage.SetActive(false);
        Monpage1.SetActive(true);
        Cardpage1.SetActive(false);
        Pplpage1.SetActive(false);
        Secretpage1.SetActive(false);
    }

    public void Cards()
    {
        CardPages.SetActive(true);
        OpeningPage.SetActive(false);
        Monpage1.SetActive(false);
        Cardpage1.SetActive(true);
        Pplpage1.SetActive(false);
        Secretpage1.SetActive(false);
    }

    public void People()
    {
        PeoplePages.SetActive(true);
        OpeningPage.SetActive(false);
        Monpage1.SetActive(false);
        Cardpage1.SetActive(false);
        Pplpage1.SetActive(true);
        Secretpage1.SetActive(false);
    }

    public void Secrets()
    {
        SecretsPages.SetActive(true);
        OpeningPage.SetActive(false);
        Monpage1.SetActive(false);
        Cardpage1.SetActive(false);
        Pplpage1.SetActive(false);
        Secretpage1.SetActive(true);
    }

    public void Controls()
    {
        ControlsPages.SetActive(true);
        OpeningPage.SetActive(false);
        Monpage1.SetActive(false);
        Cardpage1.SetActive(false);
        Pplpage1.SetActive(false);
        Secretpage1.SetActive(false);
    }

    public void Combat()
    {
        CombatPages.SetActive(true);
        OpeningPage.SetActive(false);
        Monpage1.SetActive(false);
        Cardpage1.SetActive(false);
        Pplpage1.SetActive(false);
        Secretpage1.SetActive(false);
    }

    public void ObjectiveHistory()
    {
        ObjHistoryPages.SetActive(true);
        OpeningPage.SetActive(false);
        Monpage1.SetActive(false);
        Cardpage1.SetActive(false);
        Pplpage1.SetActive(false);
        Secretpage1.SetActive(false);
    }

    public void nextPage()
    {
        if (Monpage1.activeSelf == true)
        {
            Monpage1.SetActive(false);
            Monpage2.SetActive(true);
        }
        else if (Cardpage1.activeSelf == true)
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
        else if (Pplpage1.activeSelf == true)
        {
            Pplpage1.SetActive(false);
            Pplpage2.SetActive(true);
        }
    }

    public void lastPage()
    {
        if (Monpage2.activeSelf == true)
        {
            Monpage2.SetActive(false);
            Monpage1.SetActive(true);
        }
        else if (Cardpage5.activeSelf == true)
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
        else if (Pplpage2.activeSelf == true)
        {
            Pplpage2.SetActive(false);
            Pplpage1.SetActive(true);
        }
    }

    public void CloseBook()
    {
        GuideBook.SetActive(false);
        PauseMenu.SetActive(true);
    }
    #endregion
}
