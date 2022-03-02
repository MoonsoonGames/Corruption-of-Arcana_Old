using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItems : MonoBehaviour
{
    LoadSettings loadSettings;
    MenuManager menuManager;

    int healthPotionCount;
    int arcanaPotionCount;

    PlayerController controller;

    public void Start()
    {
        controller = GameObject.FindObjectOfType<PlayerController>();

        loadSettings = LoadSettings.instance;

        healthPotionCount = loadSettings.healingPotionCount;
        arcanaPotionCount = loadSettings.arcanaPotionCount;
    }

    public void HealthPotion()
    {
        //heal
        if (loadSettings.health < controller.maxHealth && healthPotionCount > 0)
        {
            int heal = Random.Range(30, 46);
            loadSettings.health += heal;
            Debug.Log(heal + "Health healed");

            healthPotionCount -= 1;
            menuManager.HPPotionCount.text = healthPotionCount.ToString();

            if (loadSettings != null)
            {
                loadSettings.healingPotionCount = healthPotionCount;
                loadSettings.health = controller.health;
            }
        }
    }
}
