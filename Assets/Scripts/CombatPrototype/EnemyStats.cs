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

            if (health <= 0)
            {
                Die();
            }

            if (hitFX != null)
            {
                Vector3 spawnPos = new Vector3(0, 0, 0);
                Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

                spawnPos.x = transform.position.x;
                spawnPos.y = transform.position.y;
                spawnPos.z = transform.position.z;

                Instantiate(hitFX, spawnPos, spawnRot);
            }
        }
        else
        {
            health = Mathf.Clamp(health + value, 0, maxHealth);
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
}
