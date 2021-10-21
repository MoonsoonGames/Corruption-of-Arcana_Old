using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBreakdown : MonoBehaviour
{
    /* 
    activeCard;
        
    */
    // Start is called before the first frame update
    void Start()
    {   
        /*  
        playerAttackDeck = []
        playerSpellDeck = []
        playerClassDeck = []

        enemyCommonDeck = []
        enemyBossDeck = []
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        on battle start
            playerAttackDeck.randomise
            playerSpellDeck.randomise
            playerClassDeck.randomise

        on player turn
            on deck 1 click
                deck.0 = active card
                move to last position in array
                run card stats (dmg)
                enemyHealth = enemyHealth - cardDmgStat

            on deck 2 click
                deck.0 = active card
                move to last position in array
                run card stats (magic dmg)
                arcanaMagic = arcanaMagic - cardMagicStat
                enemyHealth = enemyHealth - cardDmgStat

            on deck 3 click
                deck.0 = active card
                move to last position in array
                run card stats (class effect)
                arcanaMagic = arcanaMagic - cardMagicStat
                enemyHealth = enemyHealth - cardDmgStat
        
            active card = none

        on enemy turn
            if enemy tag = common
                enemyDeck.0 = active card
                playerHealth = playerHealth - cardDmgStat

            if enemy tag = boss
                enemyDeck.0 = active card
                playerHealth = playerHealth - cardDmgStat
            
            active card = none

        if enemyHealth <= 0
            UI.load Battle Victory
        else if playerHealth <=0
            UI.load Battle Defeat
            
        */
    }
}
