using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 0.2f;

    public GameObject player;

    private PlayerStats playerStats;

    private EnemyStats enemyStats;

    public string attackName = "Slash";

    LoadSettings loadSettings;

    private void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        enemyStats = GetComponent<EnemyStats>();

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (this.tag == "bossEnemy")
        {
            if (loadSettings != null && !(loadSettings.fightingBoss))
            {
                enemyStats.ChangeHeath(10000000000, true);
            }
        }
    }

    public void TakeTurn()
    {
        playerStats.ChangeHeath(damage, true);

        Debug.Log(gameObject.name + " cast " + attackName + " for " + damage + " damage. It's really effective!");
    }
}
