using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject ExplorationUI;
    public GameObject player;
    public bool inventoryActive;

    public GameObject CombatHelpScreen;
    public GameObject UIHelpScreen;
    public GameObject GameMechHelpScreen;
    public GameObject LoreHelpScreen;

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
            }
            else
            {
                player.GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    public void CombatHelp()
    {
        Inventory.SetActive(false);
        CombatHelpScreen.SetActive(true);
    }
    public void UIHelp()
    {
        Inventory.SetActive(false);
        UIHelpScreen.SetActive(true);
    }
    public void MechanicsHelp()
    {
        Inventory.SetActive(false);
        GameMechHelpScreen.SetActive(true);
    }
    public void LoreHelp()
    {
        Inventory.SetActive(false);
        LoreHelpScreen.SetActive(true);
    }
    public void Back()
    {
        Inventory.SetActive(true);
        CombatHelpScreen.SetActive(false);
        UIHelpScreen.SetActive(false);
        GameMechHelpScreen.SetActive(false);
        LoreHelpScreen.SetActive(false);
    }
}