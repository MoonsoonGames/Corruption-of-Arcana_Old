using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinyCardDisplay : MonoBehaviour
{
    public Destiny destinyCard;

    public Text nameText;
    public Text numberText;
    public Text suitText;

    string cardName;
    string number;
    string suit;

    private void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        if (destinyCard != null)
        {
            cardName = destinyCard.cardName;
            number = destinyCard.number.ToString();
            suit = destinyCard.suit.ToString();

            nameText.text = cardName;
            numberText.text = number;
            suitText.text = suit;
        }
        else
        {
            Debug.Log("There is no destiny card");
        }
    }
}
