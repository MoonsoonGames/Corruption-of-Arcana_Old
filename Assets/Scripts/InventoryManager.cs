using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /*
     * COMP280 NOTE
     * 
     * This script was rewritten for comp280 worksheet 4 (Human-Computer interaction)
     * The script was originally built for handling the inventory and pause menu when they were combined as 1 object, now they have been split and will work in seperate scripts to be managed more efficiently
    */

    public GameObject Inventory;
    public GameObject ExplorationUI;
    public GameObject player;
    public GameObject Camera;

    //Inventory Pages
    public GameObject InvPage1;
    public GameObject InvPage2;
    public GameObject InvPage3;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.SetActive(false);
        ExplorationUI.SetActive(true);
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
            }
        }
    }

    public void CloseMenu()
    {
        Inventory.SetActive(false);
        ExplorationUI.SetActive(true);
        Cursor.visible = false;
        player.GetComponent<PlayerController>().enabled = true;
        Camera.GetComponent<PlayerCameraController>().enabled = true;
        Debug.Log("Closed Menu");
    }

    public void NextInvPage ()
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
}