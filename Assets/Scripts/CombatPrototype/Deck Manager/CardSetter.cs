using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class CardSetter : MonoBehaviour
{
    public CardParent combatCard;

    private CardParent currentCard;

    public Text cardText;

    public AbilityManager abilityManager;

    public GameObject parent;

    private Button button;

    public void Setup(CardParent spell)
    {
        button = GetComponent<Button>();
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
        currentCard = spell;
        DrawCards();
    }

    public void DrawCards()
    {
        if (cardText != null)
        {
            cardText.text = currentCard.cardName;
        }

        if (button != null && currentCard.cardImage != null)
        {
            button.image.sprite = currentCard.cardImage;
        }
    }

    public void ButtonPressed()
    {
        abilityManager.SetAbility(currentCard, this);
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