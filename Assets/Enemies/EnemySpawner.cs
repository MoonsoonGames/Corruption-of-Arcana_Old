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
            enemyName += enemy.name;
        }

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            if (CanSpawn())
            {
                if (loadSettings.enemiesKilled.Contains(enemyName))
                {
                    Debug.Log("not spawning " + enemyName);
                }
                else
                {
                    Vector3 pos = this.transform.position;
                    Quaternion rot = this.transform.rotation;

                    Instantiate(enemy, pos, rot).name = enemyName;

                    /*
                    foreach (var item in loadSettings.enemiesKilled)
                    {
                        Debug.Log(item);
                    }
                    */
                }
            }
        }

        //Destroy(this.gameObject);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f, 2f, 0.5f));
    }

    #region Enable/ Disable Spawning

    public Quest[] requireQuests;
    public QuestObjective[] requireObjectives;

    public bool requireAll = true;
    public bool destroyIfContains = true;

    public bool CanSpawn()
    {
        bool contains1 = false;
        bool containsAll = true;

        foreach (var item in requireQuests)
        {
            if (item.isComplete)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        foreach (var item in requireObjectives)
        {
            if (item.completed)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        return !((((requireAll && containsAll) || (!requireAll && contains1)) && destroyIfContains) || (((!requireAll && !containsAll) || (requireAll && !contains1)) && !destroyIfContains));
    }

    #endregion
}
