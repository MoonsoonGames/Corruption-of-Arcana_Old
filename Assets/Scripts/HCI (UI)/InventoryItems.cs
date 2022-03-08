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

    /*
     * (Repeat again for Arcana Potion)
     * ON button press
     * - IF PotionCount > 0 && current(x) < max(x)
     * -- int (x)Restore = random(30, 45)
     * -- current(x) += (x)Restore
     * -- PotionCount -= 1
     * -- PotionAmount text = PotionCount string
     * 
     * - ELSE IF PotionCount = 0
     * -- DISPLAY 'Not Enough Potions' (y)
     * -- WAIT FOR 2 secs
     * -- UNDISPLAY (y)
     * 
     * - ELSE IF current(x) = max(x)
     * -- DISPLAY 'Max (x) Reached' (z)
     * -- WAIT FOR 2 secs
     * -- UNDISPLAY (z)
     */
}
