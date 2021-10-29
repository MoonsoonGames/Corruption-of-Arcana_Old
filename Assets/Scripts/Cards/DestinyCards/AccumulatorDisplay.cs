using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccumulatorDisplay : MonoBehaviour
{
    public DeckManager accumulatorDeck;
    public List<GameObject> destinyCards;
    public Object destinyCardObject;

    public int interval = 40;

    private void Awake()
    {
        RemoveCards();
    }

    public void RemoveCards()
    {
        //clear list of cards
        foreach (var item in destinyCards)
        {
            Destroy(item);
        }
        destinyCards.Clear();
    }

    public void UpdateCards(List<Destiny> cards)
    {
        //clear list of cards
        RemoveCards();

        //make a new list of cards and instantiate objects based on the card
        for (int i = 0; i < cards.Count; i++)
        {
            Vector3 newPosition = gameObject.transform.position;

            newPosition.x += i * interval;

            destinyCards.Insert(i, Instantiate(destinyCardObject, gameObject.transform) as GameObject);

            destinyCards[i].transform.position = newPosition;

            DestinyCardDisplay display = destinyCards[i].GetComponent<DestinyCardDisplay>();

            display.destinyCard = cards[i];

            display.Setup();
        }

        //Add card values to the instantiated card
    }
}
