using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIController : MonoBehaviour
{
    public GameObject MainCombatUI;
    public GameObject PauseMenu;
    public GameObject CardMenu;
    public GameObject SettingsMenu;
    public GameObject QuitConfirm;

    // Start is called before the first frame update
    void Start()
    {
        MainCombatUI.SetActive(true);
        PauseMenu.SetActive(false);
        CardMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        QuitConfirm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MainCombatUI.SetActive(false);
            PauseMenu.SetActive(true);
        }
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

    public void Cards()
    {
        PauseMenu.SetActive(false);
        CardMenu.SetActive(true);
    }

    public void Help()
    {
        PauseMenu.SetActive(false);
        //Guidebook.SetActive(true);
    }

    public void Settings()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
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