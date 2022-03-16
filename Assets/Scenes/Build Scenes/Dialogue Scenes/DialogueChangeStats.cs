using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChangeStats : MonoBehaviour
{
    LoadSettings loadSettings;

    private void Start()
    {
        loadSettings = LoadSettings.instance;
    }

    public void RestoreHP(int restore)
    {
        if (loadSettings != null)
        {
            loadSettings.health = Mathf.Clamp(loadSettings.health + restore, 0, loadSettings.maxHealth);
        }
        else
        {
            Debug.LogError("No load settings!");
        }
    }

    public void RestoreHealthPotion(int restore)
    {
        if (loadSettings != null)
        {
            loadSettings.healingPotionCount = Mathf.Clamp(loadSettings.healingPotionCount + restore, 0, loadSettings.maxHealingPotionCount);
        }
        else
        {
            Debug.LogError("No load settings!");
        }
    }

    public void IncreaseMaxHP(int increase)
    {
        if (loadSettings != null)
        {
            loadSettings.IncreaseMaxHealth(increase);
            loadSettings.health = Mathf.Clamp(loadSettings.health + increase, 0, loadSettings.maxHealth);

        }
        else
        {
            Debug.LogError("No load settings!");
        }
    }

    public void IncreaseMaxHealthPotions(int increase)
    {
        if (loadSettings != null)
        {
            loadSettings.IncreaseMaxHealthPotions(increase);
            loadSettings.healingPotionCount = Mathf.Clamp(loadSettings.healingPotionCount + increase, 0, loadSettings.maxHealingPotionCount);

        }
        else
        {
            Debug.LogError("No load settings!");
        }
    }
}
