using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    #region Setup

    public Deck deck;

    public void Setup()
    {
        deck.Setup(this);
    }

    #endregion

    #region Deck Actions

    public void DrawCards(int number, Deck location, System.Type cardType)
    {
        //Debug.Log("Initial Card Tpye: " + cardType);

        if (cardType == null)
        {
            //for loop to go through the first n elements in deck and move them to another deck
            for (int i = 0; i < number && i < deck.cards.Count; i++)
            {
                //Debug.Log((i+1) + " out of " + number);
                if (deck.cards[i] != null)
                    deck.cards[i].DrawToDeck(location);
            }
        }
        else
        {
            //for loop to go through the first n elements in deck and move them to another deck
            for (int i = 0; i < number && i < deck.cards.Count;)
            {
                //Debug.Log((i+1) + " out of " + number);
                if (deck.cards[i] != null)
                {
                    //Debug.Log("Checked Card Tpye: " + deck.cards[i].GetType());
                    if (deck.cards[i].GetType() == cardType)
                    {
                        deck.cards[i].DrawToDeck(location);
                        i++;
                    }
                }   
            }
        }
    }

    public void TransferDeck(Deck location, System.Type cardType)
    {
        //Debug.Log("Deck: " + deck.deckName + " has : " + deck.cards.Count + " cards in it.");

        List<Card> tempCards = new List<Card>();

        foreach (var card in deck.cards)
        {
            tempCards.Add(card);
        }

        foreach (var card in tempCards)
        {
            //Debug.Log(card.cardName);
            card.DrawToDeck(location);
        }
    }

    public void Shuffle(int shuffleIntensity)
    {
        //Find a better way to shuffle cards  https://www.youtube.com/watch?v=AxJubaijQbI

        for (int i = 0; i < shuffleIntensity; i++)
            StartCoroutine(IShuffle(i * 0.000001f));
    }

    IEnumerator IShuffle(float delay)
    {
        yield return new WaitForSeconds(delay);
        deck.cards.Sort((a, b) => 1 - (2 * Random.Range(0, 2)));  //https://answers.unity.com/questions/486626/how-can-i-shuffle-alist.html
    }

    public int GetNumberOfCards()
    {
        Debug.Log(deck.cards.Count);
        return deck.cards.Count;
    }

    public void EmptyDeck()
    {
        deck.cards.Clear();
    }

    #endregion

    #region Card Actions

    public bool IsCardInDeck(Card cardToCheck)
    {
        foreach (var item in deck.cards)
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
        deck.cards.Add(cardToAdd);
    }

    public bool RemoveCard(Card cardToRemove, bool removeAll)
    {
        bool cardInDeck = false;

        foreach (var item in deck.cards)
        {
            if (item == cardToRemove)
            {
                cardInDeck = true;

                deck.cards.Remove(item);

                if (!removeAll)
                    return cardInDeck;
            }
        }

        return cardInDeck;
    }

    public bool DrawSpecificCards(Card cardToRemove, Deck location, System.Type cardType, bool drawAll)
    {
        bool cardInDeck = false;

        foreach (var item in deck.cards)
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