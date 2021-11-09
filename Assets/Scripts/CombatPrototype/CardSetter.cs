using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSetter : MonoBehaviour
{
    public E_CombatCards[] combatCards;

    private string currentCard;

    public Text cardText;

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

        if(cardText != null)
            cardText.text = currentCard;
    }

    public void ButtonPressed()
    {
        abilityManager.SetAbility(currentCard);
    }
}