using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 100;
    int health = 100;
    public int maxMana = 100;
    int mana = 100;

    public SliderValue healthSliderValue;
    public SliderValue manaSliderValue;

    public CombatManager combatManager;

    public EnemyManager enemyManager;

    public GameObject[] objectsToDisable;

    public Object hitFX;
    //public Object healFX;
    
    public Image image;
    public Color normalColour = new Color(255, 255, 255, 255);
    public Color healColour = new Color(0, 255, 0, 255);
    public Color hitColour = new Color(255, 0, 0, 255);
    Color flashColour;
    float p = 0;

    #region Received Damage Multipliers
    [Header("Received Damage Multipliers")]
    public float physicalMultiplier = 1f;
    public float emberMultiplier = 1f;
    public float staticMultiplier = 1f;
    public float bleakMultiplier = 1f;
    public float septicMultiplier = 1f;
    #endregion

    private void Start()
    {
        health = maxHealth;
        mana = maxMana;

        if (healthSliderValue != null)
        {
            healthSliderValue.slider.maxValue = maxHealth;
            healthSliderValue.slider.value = health;
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void ChangeHealth(int value, bool damage, E_DamageTypes damageType, out int damageTaken)
    {
        if (damage)
        {
            Flash(hitColour);

            damageTaken = (int)DamageResistance(value, damageType);

            health = Mathf.Clamp(health - damageTaken, 0, maxHealth);

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
            damageTaken = 0;
            Flash(healColour);
            health = Mathf.Clamp(health + value, 0, maxHealth);

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

    public float DamageResistance(float damageValue, E_DamageTypes damageType)
    {
        if (damageType == E_DamageTypes.Physical)
        {
            return damageValue * physicalMultiplier;
        }
        if (damageType == E_DamageTypes.Ember)
        {
            return damageValue * emberMultiplier;
        }
        if (damageType == E_DamageTypes.Static)
        {
            return damageValue * staticMultiplier;
        }
        if (damageType == E_DamageTypes.Bleak)
        {
            return damageValue * bleakMultiplier;
        }
        if (damageType == E_DamageTypes.Septic)
        {
            return damageValue * septicMultiplier;
        }

        return damageValue;
    }

    public Vector2 DamageResistanceVector(Vector2 damageValue, E_DamageTypes damageType)
    {
        if (damageType == E_DamageTypes.Physical)
        {
            return damageValue * physicalMultiplier;
        }
        if (damageType == E_DamageTypes.Ember)
        {
            return damageValue * emberMultiplier;
        }
        if (damageType == E_DamageTypes.Static)
        {
            return damageValue * staticMultiplier;
        }
        if (damageType == E_DamageTypes.Bleak)
        {
            return damageValue * bleakMultiplier;
        }
        if (damageType == E_DamageTypes.Septic)
        {
            return damageValue * septicMultiplier;
        }

        return damageValue;
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

    void Die()
    {
        if (hitFX != null)
        {
            Vector3 spawnPos = new Vector3(0, 0, 0);
            Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

            spawnPos.x = transform.position.x;
            spawnPos.y = transform.position.y;
            spawnPos.z = transform.position.z - 5f;

            Instantiate(hitFX, spawnPos, spawnRot);
        }

        enemyManager.enemies.Remove(GetComponent<Enemy>());
        enemyManager.EnemyKilled();

        foreach (var item in objectsToDisable)
        {
            Destroy(item);
        }
        //disable targetting, health and taking turns
    }

    public float GetMana()
    {
        return mana;
    }

    public void ChangeMana(int value, bool spend)
    {
        if (spend)
        {
            mana = Mathf.Clamp(mana - value, 0, maxMana);
        }
        else
        {
            mana = Mathf.Clamp(mana + value, 0, maxMana);
        }

        if (manaSliderValue != null)
        {
            manaSliderValue.slider.value = mana;
        }
    }

    public bool CheckMana(float value)
    {
        return mana > value;
    }

    public float HealthPercentage()
    {
        return (float)health / (float)maxHealth;
    }
}
