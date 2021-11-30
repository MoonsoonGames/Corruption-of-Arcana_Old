using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Object enemy;

    public bool boss;

    private LoadSettings loadSettings;

    public string enemyName;

    // Start is called before the first frame update
    void Start()
    {
        if (enemy != null)
        {
            enemyName = enemy.name;
        }

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            if (loadSettings.dialogueComplete)
            {
                if (loadSettings.enemiesKilled.ContainsKey(enemyName))
                {
                    if (!loadSettings.enemiesKilled[enemyName])
                    {
                        Vector3 pos = this.transform.position;
                        Quaternion rot = this.transform.rotation;

                        Instantiate(enemy, pos, rot);
                    }
                }
                else
                {
                    loadSettings.enemiesKilled.Add(name, false);

                    Vector3 pos = this.transform.position;
                    Quaternion rot = this.transform.rotation;

                    GameObject spawnedEnemy = Instantiate(enemy, pos, rot) as GameObject;

                    spawnedEnemy.name = enemyName;

                    foreach (var item in loadSettings.enemiesKilled.Keys)
                    {
                        Debug.Log(item);
                    }
                }
            }
        }

        //Destroy(this.gameObject);
    }
}
