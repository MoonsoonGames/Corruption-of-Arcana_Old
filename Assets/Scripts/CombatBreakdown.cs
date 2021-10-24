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
    public GameObject difficultyCommon;
    public GameObject difficultyBoss;
    public GameObject attackDeck;
    public GameObject spellDeck;
    public GameObject classDeck;
    public GameObject VictoryScreen;
    public GameObject DefeatScreen;

    public int turnCounter = 1;

    void update()
    {
        //if (battleStart == true)
        //{ 
              string[] attackDeck = GameObject.Find("playerAttackDeck").GetComponent<deckHandler>().playerAttackDeck;
              string[] spellDeck = GameObject.Find("playerSpellDeck").GetComponent<deckHandler>().playerSpellDeck;
              string[] classDeck = GameObject.Find("playerClassDeck").GetComponent<deckHandler>().playerClassDeck;

        //    if (enemyHealth <= 0)
        //    {
        //        //battleStart == false;
        //        //VictoryScreen.SetActive = true;
        //        //SceneManagemt.LoadLastScene; //Needs to load last scene and position
        //    }

        //    else if (playerHealth <= 0)
        //    {
        //        //battleStart == false;
        //        //DefeatScreen.SetActive = true;
        //        //SceneManagement.LoadScene(Thoth);
        //        //Transform.position(Mama reinfeld);
        //    }
        //}
    }
    void OnMouseDown()
    {
        if (turnCounter % 2 == 0) //player turn
        {
            if (tag == "attackDeck")
            {
                //activeCard.UI.SetActive = true;
                //activeCard.SetUI = playerAttackDeck.0
                //run card stats(dmg)
                //enemyHealth = enemyHealth - cardDmgStat
                //remove card from array
                //add card to last position in array
            }

            else if (tag == "spellDeck")
            {
                //activeCard.UI.SetActive = true
                //activeCard.SetUI = playerSpellDeck.0
                //run card stats(magic dmg)
                //arcanaMagic = arcanaMagic - cardMagicStat
                //enemyHealth = enemyHealth - cardDmgStat
                //remove card from array
                //add card to last position in array
            }

            else if (tag == "classDeck")
            {
                //activeCard.UI.SetActive = true
                //activeCard.SetUI = playerClassDeck.0
                //run card stats(class effect)
                //arcanaMagic = arcanaMagic - cardMagicStat
                //enemyHealth = enemyHealth - cardDmgStat
                //remove card from array
                //add card to last position in array
            }

            //activeCard.UI.SetActive = false;
        }

        else if (turnCounter % 2 != 0) //enemy turn
        {
            //enemyDeck.0 = active card;
            //playerHealth = playerHealth - cardDmgStat;
        }
    }
}
