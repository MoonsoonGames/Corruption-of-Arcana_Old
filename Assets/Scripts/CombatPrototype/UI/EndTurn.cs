using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    AbilityManager abilityManager;
    public GameObject confirmMenu;

    bool menuOpen = false;

    private void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (menuOpen)
            {
                ConfirmEndTurn();
                OpenMenu(false);
            }
            else
            {
                OpenMenu(true);
            }
        }
    }

    public void OpenMenu(bool open)
    {
        if (open)
        {
            abilityManager.ResetAbility();
            abilityManager.EnemyInfo(null);
        }

        confirmMenu.SetActive(open);
        menuOpen = open;
    }

    public void ConfirmEndTurn()
    {
        OpenMenu(false);
        abilityManager.EndTurn(0);
    }
}
