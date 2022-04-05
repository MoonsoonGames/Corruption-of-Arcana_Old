using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomFights : MonoBehaviour
{
    public Object[] enemy1, enemy2, enemy3;
    EnemyController controller;
    NavigationEvents navEvents;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<EnemyController>();

        if (controller != null)
        {
            controller.enemies[0] = enemy1[Random.Range(0, enemy1.Length)];
            controller.enemies[1] = enemy2[Random.Range(0, enemy2.Length)];
            controller.enemies[2] = enemy3[Random.Range(0, enemy3.Length)];
        }

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            GameObject enemy = controller.enemies[1] as GameObject;

            Image[] images = enemy.GetComponentsInChildren<Image>();

            foreach (var item in images)
            {
                if (item.gameObject.name == "EnemySprite")
                {
                    spriteRenderer.sprite = item.sprite;

                    break;
                }
            }
        }
    }
}
