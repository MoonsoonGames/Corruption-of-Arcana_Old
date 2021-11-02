using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSetter : MonoBehaviour
{
    public E_CombatCards[] combatCards;

    private string currentCard;

    public AbilityManager abilityManager;

    private void Start()
    {
        DrawCards();
    }

    public void DrawCards()
    {
        int rInt = Random.Range(0, combatCards.Length);
        //Debug.Log(rInt);
        currentCard = combatCards[rInt].ToString();
    }

    public void ButtonPressed()
    {
        abilityManager.SetAbility(currentCard);
    }
}