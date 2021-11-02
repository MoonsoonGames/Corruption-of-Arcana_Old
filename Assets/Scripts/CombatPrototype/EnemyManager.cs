using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<Enemy> enemies = new List<Enemy>();

    public CombatManager combatManager;

    private void Start()
    {
        Enemy[] enemiesArray = GameObject.FindObjectsOfType<Enemy>();

        foreach (var item in enemiesArray)
        {
            enemies.Add(item);
        }
    }

    public void StartEnemyTurn()
    {
        int interval = 2;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Invoke("TakeTurn", (0.25f + i) * interval);
        }

        if (combatManager!= null)
        {
            combatManager.EndTurn(false);
        }
    }
}
