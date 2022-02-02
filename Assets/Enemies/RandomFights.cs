using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFights : MonoBehaviour
{
    public Object[] enemy1, enemy2, enemy3;
    EnemyController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<EnemyController>();

        controller.enemies[0] = enemy1[Random.Range(0, enemy1.Length)];
        controller.enemies[1] = enemy2[Random.Range(0, enemy2.Length)];
        controller.enemies[2] = enemy3[Random.Range(0, enemy3.Length)];
    }
}
