using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 120;
    int health = 120;
    public int maxMana = 120;
    int mana = 120;

    public int maxPotions = 5;
    int potionCount = 3;

    public SliderValue healthSliderValue;
    public SliderValue manaSliderValue;

    public CombatManager combatManager;

    /*
    public Object hitFX;
    public Object healFX;
    */

    private LoadSettings loadSettings;

    public Image image;
    public Color normalColour = new Color(255, 255, 255, 255);
    public Color healColour = new Color(0, 255, 0, 255);
    public Color hitColour = new Color(255, 0, 0, 255);
    Color flashColour;
    float p = 0;

    private void Start()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            health = loadSettings.health;
            combatManager.HealthPointsValue.text = health.ToString();

            potionCount = loadSettings.potionCount;
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
        }

        combatManager.HealingLeft.text = potionCount.ToString();

        if (potionCount == 0)
        {
            combatManager.HealingItem.SetActive(false);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void Flash(Color newColour)
    {
        CancelInvoke();
        p = 0;

        flashColour = newColour;
        image.color = flashColour;

        InvokeRepeating("RevertColour", 0f, 0.05f);
    }

    void RevertColour()
    {
        image.color = LerpColour(flashColour, normalColour, p);

        p += 0.1f;

        if (p == 1)
        {
            CancelInvoke();
            p = 0;
        }
    }

    Color LerpColour(Color a, Color b, float i)
    {
        Color lerpColour = new Color(0, 0, 0, 0);

        lerpColour.r = Mathf.Lerp(flashColour.r, normalColour.r, i);
        lerpColour.g = Mathf.Lerp(flashColour.g, normalColour.g, i);
        lerpColour.b = Mathf.Lerp(flashColour.b, normalColour.b, i);
        lerpColour.a = Mathf.Lerp(flashColour.a, normalColour.a, i);

        return lerpColour;
    }

    public void ChangeHealth(int value, bool damage)
    {
        if (damage)
        {
            Flash(hitColour);
            health = Mathf.Clamp(health - value, 0, maxHealth);
            combatManager.HealthPointsValue.text = health.ToString();

            /*
            if (hitFX != null)
            {
                Vector3 spawnPos = new Vector3(0, 0, 0);
                Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

                spawnPos.x = transform.position.x;
                spawnPos.y = transform.position.y;
                spawnPos.z = transform.position.z - 5f;

                Instantiate(hitFX, spawnPos, spawnRot);
            }
            */

            if (health <= 0)
            {
                Die();
            }
        }
        else
        {
            Flash(healColour);
            health = Mathf.Clamp(health + value, 0, maxHealth);
            combatManager.HealthPointsValue.text = health.ToString();

            /*
            if (healFX != null)
            {
                Vector3 spawnPos = new Vector3(0, 0, 0);
                Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

                spawnPos.x = transform.position.x;
                spawnPos.y = transform.position.y;
                spawnPos.z = transform.position.z - 5f;

                Instantiate(healFX, spawnPos, spawnRot);
            }
            */
        }

        if (healthSliderValue != null)
        {
            healthSliderValue.slider.value = health;
        }
    }

    void Die()
    {
        combatManager.ShowEndScreen(false);
    }

    public int GetMana()
    {
        return mana;
    }

    public void ChangeMana(int value, bool spend)
    {
        if (spend)
        {
            mana = Mathf.Clamp(mana - value, 0, maxMana);
            combatManager.ArcanaPointsValue.text = mana.ToString();
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

    public bool CheckMana(float value)
    {
        return mana >= value;
    }

    public int GetPotions()
    {
        return potionCount;
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
            loadSettings.potionCount = potionCount;
        }
    }
}