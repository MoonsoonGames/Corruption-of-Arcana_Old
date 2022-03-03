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

    // Start is called before the first frame update
    void Start()
    {
        sceneString = sceneToLoad.ToString();
    }

    public void playGame()
    {
        sceneLoader.LoadSpecifiedScene(sceneString, LoadSceneMode.Single, dialogue);
    }
    public void credits()
    {
        mainMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }
    public void Thanks()
    {
        mainMenu.SetActive(false);
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
