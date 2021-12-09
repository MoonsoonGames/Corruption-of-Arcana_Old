using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string displayName;

    public string desciption;

    public Vector2 damage = new Vector2(18, 22);

    GameObject player;

    private PlayerStats playerStats;

    private EnemyStats enemyStats;

    public string attackName = "Slash";

    private bool canAttack = true;

    LoadSettings loadSettings;

    [TextArea(1, 3)]
    public EnemyDescription descriptionInfo;

    Sprite sprite;
    public Image image;

    private void Start()
    {
        sprite = image.sprite;

        if (displayName != null)
        {
            displayName = gameObject.name;
        }

        player = GameObject.Find("Player");

        playerStats = player.GetComponent<PlayerStats>();
        enemyStats = GetComponent<EnemyStats>();

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();
    }

    public void TakeTurn()
    {
        if (canAttack)
        {
            int randDMG = (int)Random.Range(damage.x, damage.y);

            playerStats.ChangeHeath(randDMG, true);

            Debug.Log(gameObject.name + " cast " + attackName + " for " + randDMG + " damage. It's really effective!");
        }
    }

    public void DisplayCard(bool display)
    {
        if (display)
            descriptionInfo.ReadyCard(displayName, attackName, damage, desciption, sprite);
        else
            descriptionInfo.RemoveCard();
    }
}
