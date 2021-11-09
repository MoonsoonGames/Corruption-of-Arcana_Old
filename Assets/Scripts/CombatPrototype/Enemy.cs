using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 0.2f;

    public GameObject player;

    private PlayerStats playerStats;

    public string attackName = "Slash";

    private void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
    }

    public void TakeTurn()
    {
        playerStats.ChangeHeath(damage, true);

        Debug.Log(gameObject.name + " cast " + attackName + " for " + damage + " damage. It's really effective!");
    }
}
