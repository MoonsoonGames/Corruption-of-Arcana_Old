using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[HideInInspector]
    public List<Enemy> enemies = new List<Enemy>();
    //[HideInInspector]
    public List<Targetter> targetters;

    public CombatManager combatManager;

    private void Start()
    {
        SetupLists();
    }

    public IEnumerator IDelaySetup(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetupLists();
    }

    public void SetupLists()
    {
        enemies.Clear();
        targetters.Clear();

        Enemy[] enemiesArray = GameObject.FindObjectsOfType<Enemy>();

        foreach (var item in enemiesArray)
        {
            targetters.Add(item.GetComponentInChildren<Targetter>());
        }

        foreach (var item in enemiesArray)
        {
            enemies.Add(item);

            item.GetComponent<EnemyStats>().enemyManager = this;
        }
    }

    public void TargetEnemies(bool visible)
    {
        foreach (var item in targetters)
        {
            item.SetVisibility(visible);
        }
    }

    public void StartEnemyTurn()
    {
        float interval = .75f;

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
        StartCoroutine(IDelaySetup(0.5f));

        if (enemies.Count - 1 <= 0)
        {
            combatManager.ShowEndScreen(true);
        }
    }
}