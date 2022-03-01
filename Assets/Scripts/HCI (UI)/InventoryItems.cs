using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItems : MonoBehaviour
{
    LoadSettings loadSettings;

    public Text healthPotionText;
    int healthPotionCount;

    PlayerController controller;

    public void Start()
    {
        controller = GameObject.FindObjectOfType<PlayerController>();

        loadSettings = LoadSettings.instance;

        healthPotionCount = loadSettings.healingPotionCount;
        healthPotionText.text = healthPotionCount.ToString();
    }

    public void HealthPotion()
    {
        //heal
        if (controller != null)
        {
            if (controller.health < controller.maxHealth && healthPotionCount > 0)
            {
                int heal = Random.Range(30, 46);
                controller.health += heal;

                healthPotionCount--;
                healthPotionText.text = healthPotionCount.ToString();

                if (loadSettings != null)
                {
                    loadSettings.healingPotionCount = healthPotionCount;
                    loadSettings.health = controller.health;
                }
            }
        }
    }
}
