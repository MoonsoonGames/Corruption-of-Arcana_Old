using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    /* 
    activeCard;
    */
    public static CombatManager instance;

    public Button attackDeck;
    public Button spellDeck;
    public Button classDeck;
    public Button HealthPotions;
    public GameObject VictoryScreen;
    public GameObject DefeatScreen;
    public GameObject PlayableDecks;
    public GameObject noMana;
    public GameObject Dmg;
    public GameObject Ap;
    public GameObject Healing;
    public GameObject HealingItem;
    public string playingCard;
    public Text enemyName;
    public Text ArcanaPointsValue;
    public Text HealthPointsValue;
    public Text DmgValue;
    public Text ApValue;
    public Text HealingValue;
    public Text HealingLeft;

    public PlayerStats playerStats;
    public EnemyStats enemyStats;

    public int turnCounter = 1;
    public bool battleActive = false;
    public Text turnCountText;
    public Text currentTurnText;

    public AbilityManager abilityManager;
    public EnemyManager enemyManager;

    public CardSetter[] cardSetters;

    private LoadSettings loadSettings;

    bool boss = false;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (enemyStats != null)
        {
            enemyStats.gameObject.name = enemyName.text;
        }

        DefeatScreen.SetActive(false);
        VictoryScreen.SetActive(false);
        noMana.SetActive(false);

        Dmg.SetActive(false);
        Ap.SetActive(false);
        Healing.SetActive(false);

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            boss = loadSettings.fightingBoss;
        }

        battleActive = true;

        Invoke("DelayStart", 3f);
    }

    void DelayStart()
    {
        StartTurn(true);
    }

    public void StartTurn(bool player)
    {
        abilityManager.playerTurn = player;

        /*
        if (enemyManager.enemies.Count <= 0)
            ShowEndScreen(true);
        */

        if (player)
        {
            currentTurnText.text = "Player";
            currentTurnText.color = Color.green;
            PlayableDecks.SetActive(true);
            HealingItem.SetActive(true);

            //Debug.Log("Regenerate Mana");
            playerStats.ChangeMana(15, false);

            Dmg.SetActive(false);
            Ap.SetActive(false);
            Healing.SetActive(false);

            foreach (var item in cardSetters)
            {
                item.DrawCards();
            }

            playerStats.OnTurnStartStatus();
        }
        else
        {
            currentTurnText.text = "Enemy";
            currentTurnText.color = Color.red;
            PlayableDecks.SetActive(false);
            HealingItem.SetActive(false);
            noMana.SetActive(false);

            if (enemyManager != null)
            {
                enemyManager.StartEnemyTurn();
            }
            else
            {
                EndTurn(false);
            }

            foreach (var item in enemyManager.enemies)
            {
                item.GetComponent<EnemyStats>().OnTurnStartStatus();
            }
        }
    }

    public void EndTurn(bool player)
    {
        abilityManager.playerTurn = !player;

        StartTurn(!player);

        turnCounter++;

        if (turnCountText != null)
            turnCountText.text = turnCounter.ToString();
    }

    public void ShowEndScreen(bool victory)
    {
        battleActive = false;

        if (victory)
        {
            

            VictoryScreen.SetActive(true);
            //SceneManager.LoadLast;//Needs to load last scene and position
        }
        else
        {
            if (loadSettings != null)
            {
                loadSettings.died = true;
                loadSettings.health = 120;
            }

            DefeatScreen.SetActive(true);
            //SceneManagement.LoadScene("Thoth");
            //Transform.position(Mama reinfeld);
        }
    }

    public void TargetEnemies(bool visible)
    {
        enemyManager.TargetEnemies(visible);
    }

    public void Rewards(int healing, int gold, int potions)
    {
        if (loadSettings != null && loadSettings.currentFight != null)
        {
            playerStats.ChangeHealth(healing, false, E_DamageTypes.Physical, out int damageTaken);

            playerStats.ChangePotions(potions, false);

            loadSettings.health = playerStats.GetHealth();

            loadSettings.currentGold += gold; 

            loadSettings.enemiesKilled.Add(loadSettings.currentFight);
        }

        //Debug.Log("No current fight");
    }
}