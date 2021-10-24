using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckHandler : MonoBehaviour
{
    public static deckHandler instancce;

    public string[] playerAttackDeck = new string[3];
    public string[] playerSpellDeck = new string[3];
    public string[] playerClassDeck = new string[3];

    public void Start()
    {
        string[] playerAttackDeck = new string[] {"Sword Swing", "Lunge", "Overhead Swing"};
        string[] playerSpellDeck = new string[] { "Fireball", "Purified Ice Blast", "Thunder" };
        string[] playerClassDeck = new string[] { "Beginners Luck", "On A Gamble", "Traditions" };
    }

    public void Update()
    {
        
    }
}
