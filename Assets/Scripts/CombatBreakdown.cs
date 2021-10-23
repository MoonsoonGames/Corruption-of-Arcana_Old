using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatBreakdown : MonoBehaviour
{
    /* 
    activeCard;
        
    */
    // Start is called before the first frame update
    void Start()
    {
        //Player Decks
        string[] playerAttackDeck;
        string[] playerSpellDeck;
        string[] playerClassDeck;

        //Enemy Decks
        string[] enemyCommonDeck;
        string[] enemyBossDeck;
    }

    // Update is called once per frame
    void Update()
    {
/*
        if (battleStart == true)
        {
            playerAttackDeck[].randomise;
            playerSpellDeck[].randomise;
            playerClassDeck[].randomise;
        }

        else()
        {
            return;
        }
   
        if (turnCounter%2 == 0) //player turn
            if (playerAttackDeck.IsClicked == true)
                activeCard.UI.SetActive = true
                activeCard.SetUI = playerAttackDeck.0
                run card stats (dmg)
                enemyHealth = enemyHealth - cardDmgStat
                remove card from array
                add card to last position in array

            if (playerSpellDeck.IsClicked == true)
                activeCard.UI.SetActive = true
                activeCard.SetUI = playerSpellDeck.0
                run card stats (magic dmg)
                arcanaMagic = arcanaMagic - cardMagicStat
                enemyHealth = enemyHealth - cardDmgStat
                remove card from array
                add card to last position in array

            if (playerDefenceDeck.IsClicked == true)
                activeCard.UI.SetActive = true
                activeCard.SetUI = playerClassDeck.0
                run card stats (class effect)
                arcanaMagic = arcanaMagic - cardMagicStat
                enemyHealth = enemyHealth - cardDmgStat
                remove card from array
                add card to last position in array

            activeCard.UI.SetActive = false

        else if (turnCounter%2 != 0) //enemy turn
            if (enemy.GetTag = "common")
                enemyDeck.0 = active card
                playerHealth = playerHealth - cardDmgStat

            else if (enemy.GetTag = "boss")
                enemyDeck.0 = active card
                playerHealth = playerHealth - cardDmgStat

            activeCard.UI.SetActive = false

        if enemyHealth <= 0
            UI.load Battle Victory
        else if playerHealth <=0
            UI.load Battle Defeat

*/
    }
}
