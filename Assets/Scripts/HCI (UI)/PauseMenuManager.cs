using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    /*
    * COMP280 NOTE
    * 
    * This script was rewritten for comp280 worksheet 4 (Human-Computer interaction)
    * The script was originally built for handling the inventory and pause menu when they were combined as 1 object,
    *   now they have been split and will work in seperate scripts to be managed more efficiently
    */
    public GameObject PauseMenu;
    public GameObject ExplorationUI;
    public GameObject Player;
    public GameObject Camera;
    public bool InventoryActive = false;

    public GameObject HelpScreen;

    public InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
        ExplorationUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Player.GetComponent<PlayerController>().enabled && !InventoryActive)
            {
                inventoryManager.PauseActive = true;
                PauseMenu.SetActive(true);
                ExplorationUI.SetActive(false);

                Player.GetComponent<PlayerController>().enabled = false;
                Camera.GetComponent<PlayerCameraController>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                inventoryManager.PauseActive = false;
                PauseMenu.SetActive(false);
                ExplorationUI.SetActive(true);

                Player.GetComponent<PlayerController>().enabled = true;
                Camera.GetComponent<PlayerCameraController>().enabled = true;
                Cursor.visible = false;
            }
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        ExplorationUI.SetActive(true);

        Player.GetComponent<PlayerController>().enabled = true;
        Camera.GetComponent<PlayerCameraController>().enabled = true;
        Cursor.visible = false;

        Debug.Log("Resume Playing");
    }
    
    public void Help()
    {
        PauseMenu.SetActive(false);
        HelpScreen.SetActive(true);

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
}
