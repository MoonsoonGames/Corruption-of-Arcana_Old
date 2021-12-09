using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnemySpawner : MonoBehaviour
{
    private LoadSettings loadSettings;
    public int enemyNumber = 0;
    // Start is called before the first frame update
    void Awake()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null && (enemyNumber == 0 || enemyNumber == 1 || enemyNumber == 2))
        {
            if (loadSettings.enemies[enemyNumber] != null)
            {
                GameObject enemy = Instantiate(loadSettings.enemies[enemyNumber], this.gameObject.transform) as GameObject;

                enemy.name = enemy.GetComponent<Enemy>().displayName;
            }
        }
    }
}
