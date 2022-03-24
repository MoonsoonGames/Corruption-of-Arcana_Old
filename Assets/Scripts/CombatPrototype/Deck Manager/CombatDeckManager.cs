using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDeckManager : MonoBehaviour
{
    List<CardSetter> cards = new List<CardSetter>();

    public Object card;
    List<CardParent> deck = new List<CardParent>();

    public Vector3Int cardsCount;

    public float offsetInterval = 0.5f;

    public void Setup()
    {
        CombineDecks();
    }

    void CombineDecks()
    {
        LoadSettings loadSettings = LoadSettings.instance;

        if (loadSettings != null)
        {
            foreach (var item in loadSettings.equippedWeapon.basicDeck)
            {
                deck.Add(item);
            }
            foreach (var item in loadSettings.majourArcana)
            {
                deck.Add(item);
            }
            foreach (var item in loadSettings.corruptedArcana)
            {
                deck.Add(item);
            }
        }
    }

    public void DrawTurnCards()
    {
        if (cards.Count < cardsCount.x - 2)
        {
            for (int i = cards.Count; i < cardsCount.x; i++)
            {
                SpawnCard(null);
            }
        }
        else if (cards.Count < cardsCount.y)
        {
            SpawnCard(null);
            SpawnCard(null);
        }
        else if (cards.Count >= cardsCount.z)
        {
            //draw no cards
        }
        else
        {
            SpawnCard(null);
        }

        //reorganize cards
        OffsetTransform();
    }

    public void DrawCards(int count, CardParent specificCard)
    {
        for (int i = 0; i < count; i++)
        {
            if (cards.Count < cardsCount.z)
            {
                SpawnCard(specificCard);
            }
            else
            {
                break;
            }
        }

        if (count > 0)
        {
            //reorganize cards
            OffsetTransform();
            Invoke("OffsetTransform", 0.5f);
        }
    }

    void SpawnCard(CardParent specificCard)
    {
        GameObject newCard = Instantiate(card, this.transform) as GameObject;

        CardSetter cardSetter = newCard.GetComponentInChildren<CardSetter>();

        if (specificCard != null)
        {
            cardSetter.Setup(specificCard);
        }
        else
        {
            cardSetter.Setup(DetermineCard());
        }
        
        //cardSetter.DrawCards();
        cards.Add(cardSetter);
    }

    CardParent DetermineCard()
    {
        int rInt = Random.Range(0, deck.Count);
        //Debug.Log(rInt);
        return deck[rInt];
    }

    void OffsetTransform()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].parent.transform.position = new Vector3(this.transform.position.x + (i * offsetInterval), this.transform.position.y, this.transform.position.z);
        }
    }

    public void RemoveCard(CardSetter card)
    {
        cards.Remove(card);

        Destroy(card.parent);
    }

    public void ClearCards()
    {
        foreach (var item in cards)
        {
            Destroy(item);
        }

        cards.Clear();
    }
}