using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public int maxPotions = 5;
    int potionCount = 3;

    private LoadSettings loadSettings;

    protected override void Start()
    {
        base.Start();

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            maxHealth = loadSettings.maxHealth;
            health = loadSettings.health;
            combatManager.HealthPointsValue.text = health.ToString();
            healthSliderValue.slider.maxValue = maxHealth;

            potionCount = loadSettings.healingPotionCount;
        }
        else
        {
            health = maxHealth;
            combatManager.HealthPointsValue.text = health.ToString();
        }

        mana = maxMana;
        combatManager.ArcanaPointsValue.text = mana.ToString();

        if (healthSliderValue != null)
        {
            healthSliderValue.slider.maxValue = maxHealth;
            healthSliderValue.slider.value = health;
            healthSliderValue.Setup();
            healthSliderValue.HealthFlash(HealthPercentage());
        }

        if (previewSliderValue != null)
        {
            previewSliderValue.slider.maxValue = maxHealth;
            previewSliderValue.slider.value = health;
        }

        combatManager.HealingLeft.text = potionCount.ToString();

        if (potionCount == 0)
        {
            combatManager.HealingItem.SetActive(false);
        }
    }

    protected override void Die()
    {
        combatManager.ShowEndScreen(false);
    }

    public override void ChangeMana(int value, bool spend)
    {
        if (spend)
        {
            mana = Mathf.Clamp(mana - value, 0, maxMana);
            combatManager.ArcanaPointsValue.text = mana.ToString();
            combatManager.abilityManager.RemoveApPopup(2f);
        }
        else
        {
            mana = Mathf.Clamp(mana + value, 0, maxMana);
            combatManager.ArcanaPointsValue.text = mana.ToString();
        }

        if (manaSliderValue != null)
        {
            manaSliderValue.slider.value = mana;
        }
    }

    public int GetPotions()
    {
        return potionCount;
    }

    public override void ChangeHealth(int value, bool damage, E_DamageTypes damageType, out int damageTaken, GameObject attacker, bool canBeCountered, Object attackHitFX)
    {
        base.ChangeHealth(value, damage, damageType, out damageTaken, attacker, canBeCountered, attackHitFX);

        combatManager.HealthPointsValue.text = health.ToString();

        healthSliderValue.HealthFlash(HealthPercentage());
    }

    public bool CheckPotions(int value)
    {
        return potionCount >= value;
    }

    public void ChangePotions(int value, bool spend)
    {
        if (spend)
        {
            potionCount = Mathf.Clamp(potionCount - value, 0, maxPotions);
        }
        else
        {
            potionCount = Mathf.Clamp(potionCount + value, 0, maxPotions);
        }

        if (potionCount == 0)
        {
            combatManager.HealingItem.SetActive(false);
        }

        combatManager.HealingLeft.text = potionCount.ToString();

        if (loadSettings != null)
        {
            loadSettings.healingPotionCount = potionCount;
        }
    }
}