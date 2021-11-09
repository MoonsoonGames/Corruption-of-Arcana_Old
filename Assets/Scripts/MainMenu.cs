using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsScreen;

    public void playGame()
    {
        SceneManager.LoadScene("Thoth");
    }
    public void credits()
    {
        mainMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }
    public void back()
    {
        mainMenu.SetActive(true);
        creditsScreen.SetActive(false);
    }
    public void closeGame()
    {
        Application.Quit();
    }
}
