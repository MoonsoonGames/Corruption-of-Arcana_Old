using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public int maxHealth;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "commonEnemy")
        {
            maxHealth = 40;

            if (health < maxHealth)
            {
                health = maxHealth;
            }
        }

        else if (this.tag == "bossEnemy")
        {
            maxHealth = 100;
            if (health < maxHealth)
            {
                health = maxHealth;
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
