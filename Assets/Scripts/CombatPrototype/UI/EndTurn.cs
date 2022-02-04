using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    AbilityManager abilityManager;
    public GameObject confirmMenu;

    private void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    public void OpenMenu(bool open)
    {
        confirmMenu.SetActive(open);
    }

    public void ConfirmEndTurn()
    {
        OpenMenu(false);
        abilityManager.EndTurn(0);
    }
}
