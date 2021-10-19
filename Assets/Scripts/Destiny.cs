using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destiny : Card
{
    public int number;
    private string numberDisplay;
    public E_Suits suit;
    private string suitDisplay;

    public override void Start()
    {
        base.Start();
        
        if (number == 1)
        {
            numberDisplay = "Ace";
        }
        else
        {
            numberDisplay = number.ToString();
        }

        suitDisplay = suit.ToString();

    }

    public override void OnDraw()
    {
        Debug.Log(numberDisplay + " of " + suitDisplay);
    }
}
