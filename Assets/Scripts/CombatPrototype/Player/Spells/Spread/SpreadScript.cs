using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpreadScript : MonoBehaviour
{
    CombatDeckManager combatDeckManager;

    //could be based on cards used?
    public CardParent drawCard;

    public int cardsUsed = 0;

    public Text countText;
    public Text comboText;

    public void Setup(CombatDeckManager newCombatDeckManager)
    {
        combatDeckManager = newCombatDeckManager;
        countText.text = cardsUsed.ToString();

        if (drawCard != null)
        {
            comboText.text = drawCard.cardName;
        }
        else
        {
            comboText.text = "No card";
        }
    }

    public void ResetSpread()
    {
        cardsUsed = 0;
        countText.text = cardsUsed.ToString();

        if (drawCard != null)
        {
            comboText.text = drawCard.cardName;
        }
        else
        {
            comboText.text = "No card";
        }
    }

    public void CardCast()
    {
        cardsUsed++;
        countText.text = cardsUsed.ToString();

        if (drawCard != null)
        {
            comboText.text = drawCard.cardName;
        }
        else
        {
            comboText.text = "No card";
        }

        if (cardsUsed >= 3)
        {
            ActivateCombo();
            //Building 5 could draw a different card
        }
    }

    public void ActivateCombo()
    {
        if (drawCard != null)
        {
            combatDeckManager.DrawCards(1, drawCard);
            drawCard = null;
        }
        ResetSpread();
    }
}
