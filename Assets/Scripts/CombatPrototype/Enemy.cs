using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float damage = 0.2f;

    GameObject player;

    private PlayerStats playerStats;

    private EnemyStats enemyStats;

    public string attackName = "Slash";

    private bool canAttack = true;

    LoadSettings loadSettings;

    private void Start()
    {
        player = GameObject.Find("Player");

        playerStats = player.GetComponent<PlayerStats>();
        enemyStats = GetComponent<EnemyStats>();

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();
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
