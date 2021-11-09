using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public int maxHealth;
    public int health;

    private LoadSettings loadSettings;

    // Start is called before the first frame update
    void Awake()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (this.tag == "commonEnemy")
        {
            maxHealth = 40;

            if (health < maxHealth)
            {
                health = maxHealth;
            }

            if (loadSettings != null && loadSettings.enemyKilled)
            {
                Destroy(this.gameObject);
            }

        }

        else if (this.tag == "bossEnemy")
        {
            maxHealth = 100;
            if (health < maxHealth)
            {
                health = maxHealth;
            }

            if (loadSettings != null && loadSettings.bossKilled)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            Destroy(this);
        }
    }
}
