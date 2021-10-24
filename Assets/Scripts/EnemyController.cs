using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public int maxHealth;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        if (health < maxHealth)
        {
            health = maxHealth;
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
