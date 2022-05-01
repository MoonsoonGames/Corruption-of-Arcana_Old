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

        Debug.Log(cardIndex + " | " + tutorialCards.Length);

        if (cardIndex >= tutorialCards.Length)
        {
            Debug.Log("Draw from base");
            determineCard = base.DetermineCard();
        }
        else
        {
            Debug.Log("Draw from tutorial");
            determineCard = tutorialCards[cardIndex];
        }
        cardIndex++;
        return determineCard;
    }

    void ShowTutorialMessages()
    {
        int turn = cardIndex / 3;
        if (turn < tutorialMessagesStrings.Length)
        {
            Debug.Log("Turn " + turn + "Tutorial Message: " + tutorialMessagesStrings[turn]);

            tutorialMessages.ShowMessage(tutorialTitlesStrings[turn], tutorialMessagesStrings[turn]);
        }
    }
}
