using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDeckManager : MonoBehaviour
{
    List<CardSetter> cards = new List<CardSetter>();

    public Object[] deckObjects; //Arrange in order of chance from lowest to highest, highest must be 1
    public float[] deckChances; //Arrange in order of chance from lowest to highest, highest must be 1
    Dictionary<Object, float> decks;

    public Vector2Int cardsCount;

    public float offsetInterval = 0.5f;

    private void Start()
    {
        decks = new Dictionary<Object, float>();

        for (int i = 0; i < deckObjects.Length; i++)
        {
            decks.Add(deckObjects[i], deckChances[i]);
        }

        foreach (var item in decks)
        {
            Debug.Log(item.Key + " || " + item.Value);
        }
    }

    public void DrawCards()
    {
        if (cards.Count < cardsCount.x - 1)
        {
            
            for (int i = cards.Count; i < cardsCount.x; i++)
            {
                SpawnCard();
            }
        }
        else if (cards.Count >= cardsCount.y)
        {
            //draw no cards
        }
        else
        {
            SpawnCard();
        }

        //reorganize cards
    }

    void SpawnCard()
    {
        Object test = DetermineDeck();

        if (test != null)
        {
            GameObject newCard = Instantiate(DetermineDeck(), this.transform) as GameObject;
            CardSetter cardSetter = newCard.GetComponentInChildren<CardSetter>();
            cardSetter.DrawCards();
            cards.Add(cardSetter);

            OffsetTransform(newCard);
        }
        else
        {
            Debug.LogError("Issue with getting a deck");
        }
    }

    Object DetermineDeck()
    {
        float rFloat = Random.Range(0f, 1f);
        Debug.Log(rFloat);

        foreach (var item in decks)
        {
            if (rFloat <= item.Value)
            {
                Debug.Log(item.Key);
                return item.Key;
            }
        }

        return null;
    }

    void OffsetTransform(GameObject card)
    {
        card.transform.position = new Vector3(card.transform.position.x + (cards.Count * offsetInterval), card.transform.position.y, card.transform.position.z);
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