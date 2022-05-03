using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsScreen;
    public GameObject thanksScreen;

    public E_Levels sceneToLoad;
    string sceneString;

    public SceneLoader sceneLoader;
    public Object dialogue;

    LoadSettings loadSettings;

    // Start is called before the first frame update
    void Start()
    {
        loadSettings = LoadSettings.instance;
        sceneString = sceneToLoad.ToString();
    }

    public void playGame(bool load)
    {
        if (load)
        {
            loadSettings.LoadData();
        }
        else
        {
            loadSettings.SaveData();
        }
        sceneLoader.LoadSpecifiedScene(sceneString, LoadSceneMode.Single, dialogue);
    }
    public void credits()
    {
        mainMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }
    public void Thanks()
    {
        creditsScreen.SetActive(false);
        thanksScreen.SetActive(true);
    }
    public void Menu()
    {
        mainMenu.SetActive(true);
        thanksScreen.SetActive(false);
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
