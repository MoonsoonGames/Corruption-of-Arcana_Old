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

    public GameObject difficultyCommon;
    public GameObject difficultyBoss;
    public Button attackDeck;
    public Button spellDeck;
    public Button classDeck;
    public GameObject VictoryScreen;
    public GameObject DefeatScreen;

    public PlayerController enemyDifficulty;

    public int turnCounter = 1;

    private void Start()
    {

    }
    void update()
    {
        //while (battleStart == true)
        //{
        //    if (EnemyController.instance.health <= 0)
        //    {
        //        battleStart == false;
        //        VictoryScreen.SetActive(true);
        //        SceneManager.LoadScene("Thoth");//Needs to load last scene and position
        //    }

        //    else if (PlayerController.instance.health <= 0)
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
