using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Object enemy;

    public bool boss;

    private LoadSettings loadSettings;

    // Start is called before the first frame update
    void Start()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            if ((boss &! loadSettings.bossKilled) || (!boss &! loadSettings.enemyKilled))
            {
                Instantiate(enemy);
            }
        }

        //Destroy(this.gameObject);
    }
}
