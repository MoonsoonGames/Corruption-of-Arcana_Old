using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accumulator : MonoBehaviour
{
    public E_Suits fortuneSuit;
    public E_Suits misfortuneSuit;

    private DeckManager deckManager;
    private int destinyValue = 0;
    private int fortuneCount = 0;
    private int misfortuneCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        deckManager = GetComponent<DeckManager>();

        deckManager.deck.onDrawDelegate += CountCards;
    }

    public void CountCards()
    {
        List<Destiny> cards = new List<Destiny>();

        foreach (var item in deckManager.deck.cards)
        {
            if (item.GetType() == typeof(Destiny))
            {
                cards.Add((Destiny)item);
            }
        }

        destinyValue = 0;
        fortuneCount = 0;
        misfortuneCount = 0;

        foreach (var item in cards)
        {
            destinyValue += item.number;

            if (item.suit == fortuneSuit)
            {
                fortuneCount++;
            }
            else if (item.suit == misfortuneSuit)
            {
                misfortuneCount++;
            }
        }

        Debug.Log("total value: " + destinyValue + " | fortune value: " + fortuneCount + " | misfortune value: " + misfortuneCount);
    }

    Dictionary<bool, int> Accumulate()
    {
        Dictionary<bool, int> result = new Dictionary<bool, int>();



        return result;
    }
}
