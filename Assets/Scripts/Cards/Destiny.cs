using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Destiny", order = 1)]
public class Destiny : Card
{
    public int number;
    private string numberDisplay;
    public E_Suits suit;
    private string suitDisplay;

    public override void Setup()
    {
        base.Setup();
        
        if (number == 1)
        {
            numberDisplay = "Ace";
        }
        else
        {
            numberDisplay = number.ToString();
        }

        suitDisplay = suit.ToString();

        cardName = numberDisplay + " of " + suitDisplay;
    }

    public override void OnDraw()
    {
        Debug.Log(cardName);
    }
}
