using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatHandler : MonoBehaviour
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

    public Slider enemyHealth;
    public Slider playerHealth;
    public Slider playerArcana;

    public PlayerController enemyDifficulty;

    public int turnCounter = 1;
    public bool battleActive = false;

    public void Start()
    {
        EnemyController.instance.gameObject.name = enemyName.text;
        DefeatScreen.SetActive(false);
        VictoryScreen.SetActive(false);
        playerHealth.value = PlayerController.instance.maxHealth;
        playerArcana.value = PlayerController.instance.maxArcana;
    }
    void update()
    {
        while (battleActive == true)
        {
            if (PlayerController.instance.health <= 0)
            {
                battleActive = false;
                DefeatScreen.SetActive(true);
                //SceneManagement.LoadScene("Thoth");
                //Transform.position(Mama reinfeld);
            }

            playerHealth.value = PlayerController.instance.health;
            playerArcana.value = PlayerController.instance.arcana;
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
