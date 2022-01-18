using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class CardSetter : MonoBehaviour
{
    public CardParent[] combatCards;

    private CardParent currentCard;

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
        currentCard = combatCards[rInt];

        if (cardText != null)
        {
            cardText.text = currentCard.cardName;
        }
    }

    public void ButtonPressed()
    {
        abilityManager.SetAbility(currentCard);
    }


    //https://www.code-helper.com/answers/add-spaces-between-words-unity
    public string AddSpacesToSentence(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";

        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }
}