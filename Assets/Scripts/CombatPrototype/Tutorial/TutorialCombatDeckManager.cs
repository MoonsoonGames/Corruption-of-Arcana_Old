using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCombatDeckManager : CombatDeckManager
{
    int cardIndex = 0;
    public CardParent[] tutorialCards;

    public TutorialMessages tutorialMessages;
    public string[] tutorialTitlesStrings;
    [TextArea(3, 10)]
    public string[] tutorialMessagesStrings;

    protected override CardParent DetermineCard()
    {
        ShowTutorialMessages();
        CardParent determineCard;

        if (cardIndex >= tutorialCards.Length)
        {
            determineCard = base.DetermineCard();
        }
        else
        {
            determineCard = tutorialCards[cardIndex];
        }
        cardIndex++;
        return determineCard;
    }

    void ShowTutorialMessages()
    {
        int turn = cardIndex / 3;
        Debug.Log("Turn " + turn + "Tutorial Message: " + tutorialMessagesStrings[turn]);

        tutorialMessages.ShowMessage(tutorialTitlesStrings[turn], tutorialMessagesStrings[turn]);
    }
}
