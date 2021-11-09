using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth = 1f;
    float health = 1f;
    public float maxMana = 1f;
    float mana = 1f;

    public SliderValue healthSliderValue;
    public SliderValue manaSliderValue;

    public CombatManager combatManager;

    public EnemyManager enemyManager;

    public GameObject[] objectsToDisable;

    public Object hitFX;
    public Object healFX;

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

    public void ChangeHeath(float value, bool damage)
    {
        if (damage)
        {
            health = Mathf.Clamp(health - value, 0, maxHealth);

            if (hitFX != null)
            {
                Vector3 spawnPos = new Vector3(0, 0, 0);
                Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

                spawnPos.x = transform.position.x;
                spawnPos.y = transform.position.y;
                spawnPos.z = transform.position.z - 5f;

                Instantiate(hitFX, spawnPos, spawnRot);
            }

            if (health <= 0)
            {
                Die();
            }
        }
        else
        {
            health = Mathf.Clamp(health + value, 0, maxHealth);

            if (healFX != null)
            {
                Vector3 spawnPos = new Vector3(0, 0, 0);
                Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

                spawnPos.x = transform.position.x;
                spawnPos.y = transform.position.y;
                spawnPos.z = transform.position.z - 5f;

                Instantiate(healFX, spawnPos, spawnRot);
            }
        }

        if (healthSliderValue != null)
        {
            healthSliderValue.slider.value = health;
        }
    }

    void Die()
    {
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

    public void ChangeMana(float value, bool spend)
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
}
