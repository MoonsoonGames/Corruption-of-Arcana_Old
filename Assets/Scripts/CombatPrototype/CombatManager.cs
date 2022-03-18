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

    #region UI

    public GameObject VictoryScreen;
    public GameObject DefeatScreen;
    public GameObject CombatCanvas;
    public GameObject PlayableDecks;
    public GameObject noMana;
    //public GameObject Dmg;
    //public GameObject Ap;
    //public GameObject Healing;
    public GameObject HealingItem;
    public string playingCard;
    public Text enemyName;
    public Text ArcanaPointsValue;
    public Text HealthPointsValue;
    //public Text DmgValue;
    //public Text ApValue;
    //public Text HealingValue;
    public Text HealingLeft;

    public GameObject endTurnButton;

    #endregion

    public PlayerStats playerStats;
    public EnemyStats enemyStats;

    public AbilityManager abilityManager;
    public EnemyManager enemyManager;

    public CombatDeckManager combatDeckManager;
    private LoadSettings loadSettings;

    bool boss = false;

    public int arcanaRegen = 20;
    public Image bgImage;

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

        endTurnButton.SetActive(false);

        combatDeckManager.Setup();

        loadSettings = LoadSettings.instance;

        SetBackground();

        if (loadSettings != null)
        {
            boss = loadSettings.fightingBoss;
        }

        Invoke("DelayStart", 3f);
    }

    void DelayStart()
    {
        StartTurn(true);
    }

    public void DrawCards()
    {
        if (combatDeckManager != null)
        {
            combatDeckManager.DrawTurnCards();
        }
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
            HealingItem.SetActive(true);
            PlayableDecks.SetActive(true);
            endTurnButton.SetActive(true);

            //Debug.Log("Regenerate Mana");
            playerStats.ChangeMana(arcanaRegen, false);

            DrawCards();

            playerStats.OnTurnStartStatus();
        }
        else
        {
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

        abilityManager.ResetAbility();

        if (player)
        {
            playerStats.OnTurnEndStatus();
            endTurnButton.SetActive(false);
        }
        else
        {
            foreach (var item in enemyManager.enemies)
            {
                item.GetComponent<EnemyStats>().OnTurnEndStatus();
            }
        }

        StartTurn(!player);
    }

    public void ShowEndScreen(bool victory)
    {
        if (CombatCanvas != null)
        {
            CombatCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("No Combat Canvas");
        }

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

    public void TargetEnemies(bool visible, CardParent spell)
    {
        enemyManager.TargetEnemies(visible, spell);
    }

    public void SetBackground()
    {
        if (bgImage != null && loadSettings.background != null)
        {
            bgImage.sprite = loadSettings.background;
        }

        loadSettings.background = null;
    }

    public void Rewards(int healing)
    {
        if (loadSettings != null && loadSettings.currentFight != null)
        {
            playerStats.ChangeHealth(healing, false, E_DamageTypes.Physical, out int damageTaken, this.gameObject, false, null);

            loadSettings.health = playerStats.GetHealth();

            loadSettings.enemiesKilled.Add(loadSettings.currentFight);

            if (loadSettings.fightingBoss)
            {
                loadSettings.bossesKilled.Add(loadSettings.currentFight);
            }
        }
    }
}