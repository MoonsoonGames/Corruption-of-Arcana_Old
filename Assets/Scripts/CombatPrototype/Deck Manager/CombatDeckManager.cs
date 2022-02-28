using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDeckManager : MonoBehaviour
{
    List<CardSetter> cards = new List<CardSetter>();

    public Object card;
    public CardParent[] basicArcana, majourArcana, corruptedArcana;
    public List<CardParent> deck;

    public Vector3Int cardsCount;

    public float offsetInterval = 0.5f;

    private void Start()
    {
        CombineDecks();
    }

    void CombineDecks()
    {
        Debug.Log("Combining Decks");
        foreach (var item in basicArcana)
        {
            deck.Add(item);
        }
        foreach (var item in majourArcana)
        {
            deck.Add(item);
        }
        foreach (var item in corruptedArcana)
        {
            deck.Add(item);
        }
    }

    public void DrawCards()
    {
        if (cards.Count < cardsCount.x - 2)
        {
            for (int i = cards.Count; i < cardsCount.x; i++)
            {
                SpawnCard();
            }
        }
        else if (cards.Count < cardsCount.y)
        {
            SpawnCard();
            SpawnCard();
        }
        else if (cards.Count >= cardsCount.z)
        {
            //draw no cards
        }
        else
        {
            SpawnCard();
        }

        //reorganize cards
        OffsetTransform();
    }

    void SpawnCard()
    {
        GameObject newCard = Instantiate(card, this.transform) as GameObject;

        CardSetter cardSetter = newCard.GetComponentInChildren<CardSetter>();
        cardSetter.Setup(DetermineCard());
        cards.Add(cardSetter);
    }

    CardParent DetermineCard()
    {
        int rInt = Random.Range(0, deck.Count - 1);
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