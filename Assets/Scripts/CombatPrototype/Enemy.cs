using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float damage = 0.2f;

    public GameObject player;

    private PlayerStats playerStats;

    private EnemyStats enemyStats;

    public string attackName = "Slash";

    private bool canAttack = true;

    LoadSettings loadSettings;

    public Image sprite;

    public Sprite replaceSprite;

    private void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        enemyStats = GetComponent<EnemyStats>();
        sprite = GetComponentInChildren<Image>();

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (this.tag == "bossEnemy")
        {
            if (loadSettings != null && !(loadSettings.fightingBoss))
            {
                canAttack = false;

                sprite.sprite = replaceSprite;
            }
        }
    }

    public void TakeTurn()
    {
        if (canAttack)
        {
            playerStats.ChangeHeath(damage, true);

            Debug.Log(gameObject.name + " cast " + attackName + " for " + damage + " damage. It's really effective!");
        }
    }
}
