using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    #region Setup

    public List<Card> cards;
    //Character owner
    public E_Decks deckType;
    private string deckTypeDisplay;

    public Deck hand;

    // Start is called before the first frame update
    void Start()
    {
        deckTypeDisplay = deckType.ToString();

        if (deckTypeDisplay == "Dealer")
        {
            Destiny[] cardsInScene = GameObject.FindObjectsOfType<Destiny>();

            foreach (var item in cardsInScene)
            {
                cards.Add(item);
            }
        }

        foreach (var item in cards)
        {
            item.SetDeck(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hand != null)
        {
            DrawCards(1, hand, null);
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && hand != null)
        {
            Shuffle(10);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && hand != null)
        {
            hand.DrawCards(hand.GetNumberOfCards(), this, null);
        }
    }

    #endregion

    #region Deck Actions

    public void DrawCards(int number, Deck location, Card cardType)
    {
        //for loop to go through the first n elements in deck and move them to another deck
        for (int i = 0; i < number && i < cards.Count; i++)
        {
            Debug.Log((i+1) + " out of " + number);
            if (cards[i] != null)
                cards[i].DrawToDeck(location);
        }
    }

    public void Shuffle(int shuffleIntensity)
    {
        for (int i = 0; i < shuffleIntensity; i++)
            cards.Sort((a, b) => 1 - (2 * Random.Range(0, 2)));  //https://answers.unity.com/questions/486626/how-can-i-shuffle-alist.html
    }

    public int GetNumberOfCards()
    {
        Debug.Log(cards.Count);
        return cards.Count;
    }

    public void EmptyDeck()
    {
        cards.Clear();
    }

    #endregion

    #region Card Actions

    public bool IsCardInDeck(Card cardToCheck)
    {
        foreach (var item in cards)
        {
            if (item == cardToCheck)
            {
                return true;
            }
        }

        return false;
    }

    public void AddCard(Card cardToAdd)
    {
        cards.Add(cardToAdd);
    }

    public bool RemoveCard(Card cardToRemove, bool removeAll)
    {
        bool cardInDeck = false;

        foreach (var item in cards)
        {
            if (item == cardToRemove)
            {
                cardInDeck = true;

                cards.Remove(item);

                if (!removeAll)
                    return cardInDeck;
            }
        }

        return cardInDeck;
    }

    public bool DrawSpecificCards(Card cardToRemove, Deck location, Card cardType, bool drawAll)
    {
        bool cardInDeck = false;

        foreach (var item in cards)
        {
            if (item == cardToRemove)
            {
                cardInDeck = true;

                item.DrawToDeck(location);

                if (!drawAll)
                    return cardInDeck;
            }
        }

        return cardInDeck;
    }

    #endregion

    #region Owner Actions

    /* Implement when Charcater class is implemented
    public Character GetOwner()
    {
        return owner;
    }

    public void SetOwner(character newCharacter)
    {
        owner = newCharacter;
    }
    */

    #endregion
}