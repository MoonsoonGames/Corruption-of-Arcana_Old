using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject ExplorationUI;
    public GameObject player;
    public GameObject Camera;
    public bool inventoryActive;

    public GameObject CombatHelpScreen;
    public GameObject UIHelpScreen;
    public GameObject GameMechHelpScreen;
    public GameObject LoreHelpScreen;
    public GameObject NavigationHelp1;
    public GameObject NavigationHelp2;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.SetActive(false);
        CombatHelpScreen.SetActive(false);
        UIHelpScreen.SetActive(false);
        GameMechHelpScreen.SetActive(false);
        LoreHelpScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory.SetActive(!Inventory.activeSelf);
            ExplorationUI.SetActive(!ExplorationUI.activeSelf);

            if (player.GetComponent<PlayerController>().enabled == true)
            {
                player.GetComponent<PlayerController>().enabled = false;
                Camera.GetComponent<PlayerCameraController>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                player.GetComponent<PlayerController>().enabled = true;
                Camera.GetComponent<PlayerCameraController>().enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void CombatHelp()
    {
        Inventory.SetActive(false);
        CombatHelpScreen.SetActive(true);
        Debug.Log("Combat help loaded");
    }
    public void UIHelp()
    {
        Inventory.SetActive(false);
        UIHelpScreen.SetActive(true);
        Debug.Log("UI help loaded");
    }
    public void MechanicsHelp()
    {
        Inventory.SetActive(false);
        GameMechHelpScreen.SetActive(true);
        Debug.Log("Mechanics help loaded");
    }
    public void LoreHelp()
    {
        Inventory.SetActive(false);
        LoreHelpScreen.SetActive(true);
        Debug.Log("Lore help loaded");
    }
    public void Back()
    {
        Inventory.SetActive(true);
        CombatHelpScreen.SetActive(false);
        UIHelpScreen.SetActive(false);
        GameMechHelpScreen.SetActive(false);
        LoreHelpScreen.SetActive(false);
        Debug.Log("Inventory loaded");
    }
    public void nextPage()
    {
        NavigationHelp1.SetActive(false);
        NavigationHelp2.SetActive(true);
        Debug.Log("next page loaded");
    }
    public void backPage()
    {
        NavigationHelp1.SetActive(true);
        NavigationHelp2.SetActive(false);
        Debug.Log("previous page loaded");
    }
    public void quitButton()
    {
        Application.Quit();
    }
}