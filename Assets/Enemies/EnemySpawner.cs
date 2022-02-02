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
            if (loadSettings.dialogueComplete)
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
}
