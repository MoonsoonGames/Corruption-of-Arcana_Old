using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsScreen;

    public E_Levels sceneToLoad;
    string sceneString;

    // Start is called before the first frame update
    void Start()
    {
        sceneString = sceneToLoad.ToString();
    }

    public void playGame()
    {
        SceneManager.LoadScene(sceneString);
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
