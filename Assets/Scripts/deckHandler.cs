using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckHandler : MonoBehaviour
{
    public static deckHandler instance;

    public int cardDMG;
    public int cardArcana;

    public string[] playerAttackDeck = new string[3];
    public string[] playerSpellDeck = new string[3];
    public string[] playerClassDeck = new string[3];

    public string[] enemyDeck = new string[2];

    public void Start()
    {
        string[] playerAttackDeck = new string[] {"Sword Swing", "Lunge", "Overhead Swing"};
        string[] playerSpellDeck = new string[] { "Fireball", "Purified Ice Blast", "Thunder" };
        string[] playerClassDeck = new string[] { "Beginners Luck", "On A Gamble", "Traditions" };

        string[] enemyDeck = new string[] {"Furious Swing", "Whack"};
    }

    public void Update()
    {
        //Attack Deck Cards
        if (CombatHandler.instance.playingCard == "Sword Swing")
        {
            cardDMG = 8;
            cardArcana = 0;
        }
        else if (CombatHandler.instance.playingCard == "Lunge")
        {
            cardDMG = 7;
            cardArcana = 0;
        }
        else if (CombatHandler.instance.playingCard == "Overhead Swing")
        {
            cardDMG = 12;
            cardArcana = 0;
        }

        //Spell Deck Cards
        if (CombatHandler.instance.playingCard == "Fireball")
        {
            cardDMG = 5;
            cardArcana = 2;
        }
        else if (CombatHandler.instance.playingCard == "Purified Ice Blast")
        {
            cardDMG = 15;
            cardArcana = 10;
        }
        else if (CombatHandler.instance.playingCard == "Thunder")
        {
            cardDMG = 8;
            cardArcana = 5;
        }

        //Class Deck Cards
        if (CombatHandler.instance.playingCard == "Beginners Luck")
        {
            cardDMG = 12;
            cardArcana = 0;
        }
        else if (CombatHandler.instance.playingCard == "On A Gamble")
        {
            cardDMG = 4;
            cardArcana = 1;
        }
        else if (CombatHandler.instance.playingCard == "Traditions")
        {
            cardDMG = 5;
            cardArcana = 1;
        }

        //Enemy Deck
        if (CombatHandler.instance.playingCard == "Furious Swing")
        {
            if (EnemyController.instance.gameObject.tag == "commonEnemy")
            {
                cardDMG = 6;
            }
            else if (EnemyController.instance.gameObject.tag == "bossEnemy")
            {
                cardDMG = 10;
            }
        }
        else if (CombatHandler.instance.playingCard == "Whack")
        {
            if (EnemyController.instance.gameObject.tag == "commonEnemy")
            {
                cardDMG = 5;
            }
            else if (EnemyController.instance.gameObject.tag == "bossEnemy")
            {
                cardDMG = 11;
            }
        }
    }
}
