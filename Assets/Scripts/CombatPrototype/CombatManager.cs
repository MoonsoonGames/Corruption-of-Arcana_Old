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
    public static CombatHandler instance;

    public Image difficultyCommon;
    public Image difficultyBoss;
    public Button attackDeck;
    public Button spellDeck;
    public Button classDeck;
    public GameObject VictoryScreen;
    public GameObject DefeatScreen;
    public string playingCard;
    public Text enemyName;

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
        if (enemyStats != null)
        {
            enemyStats.gameObject.name = enemyName.text;
        }

        DefeatScreen.SetActive(false);
        VictoryScreen.SetActive(false);

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            boss = loadSettings.fightingBoss;
        }

        battleActive = true;

        StartTurn(true);
    }

    public void StartTurn(bool player)
    {
        abilityManager.playerTurn = player;

        if (player)
        {
            currentTurnText.text = "Player";

            Debug.Log("Regenerate Mana");
            playerStats.ChangeMana(0.15f, false);

            foreach (var item in cardSetters)
            {
                item.DrawCards();
            }
        }
        else
        {
            currentTurnText.text = "Enemy";

            if (enemyManager != null)
            {
                enemyManager.StartEnemyTurn();
            }
            else
            {
                EndTurn(false);
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
            if (loadSettings != null)
            {
                loadSettings.health = playerStats.GetHealth();

                if (boss)
                {
                    loadSettings.bossKilled = true;
                }
                else
                {
                    loadSettings.enemyKilled = true;
                }
            }


            VictoryScreen.SetActive(true);
            //SceneManager.LoadLast;//Needs to load last scene and position
        }
        else
        {
            if (loadSettings != null)
            {
                loadSettings.bossKilled = false;
                loadSettings.enemyKilled = false;
                loadSettings.died = true;
                loadSettings.health = 1.2f;
            }

            DefeatScreen.SetActive(true);
            //SceneManagement.LoadScene("Thoth");
            //Transform.position(Mama reinfeld);
        }
    }

    public void OnMouseDown()
    {
        if (turnCounter % 2 == 0) //player turn
        {
            if (tag == "attackDeck")
            {
                //activeCard.UI.SetActive = true;
                //activeCard.SetUI = deckHandler.instance.playerAttackDeck[0];
                playingCard = deckHandler.instance.playerAttackDeck[0];
                EnemyController.instance.health = EnemyController.instance.health - deckHandler.instance.cardDMG;
                PlayerController.instance.arcana = PlayerController.instance.arcana - deckHandler.instance.cardArcana;
                //remove card from array
                //add card to last position in array
                turnCounter += 1;
            }

            else if (tag == "spellDeck")
            {
                //activeCard.UI.SetActive = true;
                //activeCard.SetUI = deckHandler.instance.playerSpellDeck[0];
                playingCard = deckHandler.instance.playerSpellDeck[0];
                EnemyController.instance.health = EnemyController.instance.health - deckHandler.instance.cardDMG;
                PlayerController.instance.arcana = PlayerController.instance.arcana - deckHandler.instance.cardArcana;
                //remove card from array
                //add card to last position in array
                turnCounter += 1;
            }

            else if (tag == "classDeck")
            {
                //activeCard.UI.SetActive = true;
                //activeCard.SetUI = deckHandler.instance.playerClassDeck[0];
                playingCard = deckHandler.instance.playerClassDeck[0];
                EnemyController.instance.health = EnemyController.instance.health - deckHandler.instance.cardDMG;
                PlayerController.instance.arcana = PlayerController.instance.arcana - deckHandler.instance.cardArcana;
                //remove card from array
                //add card to last position in array
                turnCounter += 1;
            }

            //activeCard.UI.SetActive = false;
        }

        else if (turnCounter % 2 != 0) //enemy turn
        {
            playingCard = deckHandler.instance.enemyDeck[0];
            PlayerController.instance.health = PlayerController.instance.health - deckHandler.instance.cardDMG;
        }
    }
}
