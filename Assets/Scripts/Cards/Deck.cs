using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDeck", menuName = "Cards/Deck", order = 0)]
public class Deck : ScriptableObject
{
    #region Setup

    public string deckName;
    public List<Card> cards;
    //Character owner
    public E_Decks deckType;
    private string deckTypeDisplay;

    private DeckManager manager;

    public void Setup(DeckManager newManager)
    {
        deckTypeDisplay = deckType.ToString();

        manager = newManager;

        foreach (var item in cards)
        {
            item.Setup();
            item.SetDeck(this);
        }
    }

    #endregion

    public DeckManager GetManager()
    {
        return manager;
    }
}