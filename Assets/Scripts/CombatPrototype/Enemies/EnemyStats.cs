using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats
{
    public EnemyManager enemyManager;

    public GameObject[] objectsToDisable;

    public bool boss = false;

    Enemy enemy;

    public Text healthText;

    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();

        health = maxHealth;
        mana = maxMana;

        if (healthSliderValue != null)
        {
            healthSliderValue.slider.maxValue = maxHealth;
            healthSliderValue.slider.value = health;
        }

        if (previewSliderValue != null)
        {
            previewSliderValue.slider.maxValue = maxHealth;
            previewSliderValue.slider.value = health;
        }

        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    public override void ChangeHealth(int value, bool damage, E_DamageTypes damageType, out int damageTaken, GameObject attacker, bool canBeCountered, Object attackHitFX)
    {
        base.ChangeHealth(value, damage, damageType, out damageTaken, attacker, canBeCountered, attackHitFX);

        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    protected override void Die()
    {
        if (killFX != null)
        {
            Vector3 spawnPos = new Vector3(0, 0, 0);
            Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

            spawnPos.x = transform.position.x;
            spawnPos.y = transform.position.y;
            spawnPos.z = transform.position.z - 5f;

            Instantiate(killFX, spawnPos, spawnRot);
        }

        if (boss && enemy != null)
        {
            GameObject.FindObjectOfType<LoadSettings>().AddToGuidebook(enemy.displayName);
        }

        enemyManager.enemies.Remove(GetComponent<Enemy>());
        enemyManager.EnemyKilled();

        foreach (var item in objectsToDisable)
        {
            Destroy(item);
        }
        //disable targetting, health and taking turns
    }

    public override void ApplyStatus(StatusParent status, GameObject caster, int duration)
    {
        base.ApplyStatus(status, caster, duration);

        if (status.revealEntry && enemy != null)
        {
            GameObject.FindObjectOfType<LoadSettings>().AddToGuidebook(enemy.displayName);
            GameObject.FindObjectOfType<AbilityManager>().EnemyInfo(enemy);

            ShowStatsButton[] statsButtons = GameObject.FindObjectsOfType<ShowStatsButton>();

            foreach (var item in statsButtons)
            {
                item.CheckReveal();
            }
        }
    }
}