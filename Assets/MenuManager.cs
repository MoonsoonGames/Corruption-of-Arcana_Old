using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject ExplorationUI;
    public GameObject PauseMenuUI;
    public GameObject Player;
    public GameObject PlayerCamera;

    public GameObject SettingsMenu;
    public GameObject HelpMenu;
    public GameObject PlayerStatsUI;

    public GameObject MainMenuConfirmScreen;
    public GameObject QuitConfirmScreen;
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
    }

    // Update is called once per frame
    void Update()
    {
        #region Open Hotkeys
        //Esc to OPEN menu
        if (PauseMenuUI.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            //turn off Exploration UI
            ExplorationUI.SetActive(false);
            //turn on Pause Menu UI
            PauseMenuUI.SetActive(true);
            //turn on player Stats
            PlayerStatsUI.SetActive(true);

            //Freeze player and camera
            Player.GetComponent<PlayerController>().canMove = false;

            //unlock mouse - not confined
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //Debug
            Debug.Log("Pause Menu Active");
        }
        //M to open local map
            //turn off Exploration UI
            //turn on local map
            //freeze player and camera
            //keep mouse hidden/locked in place
            //Debug

        #endregion

        #region Close Hotkeys
        //Esc to CLOSE menu
        if (PauseMenuUI.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            //turn on Exploration UI
            ExplorationUI.SetActive(true);
            //turn off Pause Menu UI
            PauseMenuUI.SetActive(false);
            //Unfreeze player and camera
            Player.GetComponent<PlayerController>().canMove = true;

            //hide mouse/locked in place
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //Debug
            Debug.Log("Pause Menu Deactive");
        }
        //M to close local map
            //turn on Exploration UI
            //turn off local map
            //unfreeze player and camera
            //keep mouse hidden/locked in place
            //Debug
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

    #endregion

}