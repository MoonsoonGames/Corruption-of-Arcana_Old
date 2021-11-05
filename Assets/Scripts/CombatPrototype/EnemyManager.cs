using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<Enemy> enemies = new List<Enemy>();

    public CombatManager combatManager;

    int enemiesKilled = 0;

    private void Start()
    {
        Enemy[] enemiesArray = GameObject.FindObjectsOfType<Enemy>();

        foreach (var item in enemiesArray)
        {
            enemies.Add(item);

            item.GetComponent<EnemyStats>().enemyManager = this;
        }
    }

    public void StartEnemyTurn()
    {
        float interval = 0.75f;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
                enemies[i].Invoke("TakeTurn", (0.6f + i) * interval);
        }

        Invoke("EndEnemyTurn", interval * (0.4f + enemies.Count));
    }

    private void EndEnemyTurn()
    {
        if (combatManager != null)
        {
            combatManager.EndTurn(false);
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled >= enemies.Count)
        {
            combatManager.ShowEndScreen(true);
        }
    }
}