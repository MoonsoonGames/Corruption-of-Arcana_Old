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

    #region Delegate Functions

    public delegate void DrawDelegate();
    public DrawDelegate onDrawDelegate;

    #endregion

    public void Setup(DeckManager newManager)
    {
        onDrawDelegate += OnDraw;

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

    public string GetDeckType()
    {
        return deckTypeDisplay;
    }

    public virtual void OnDraw()
    {
        //Debug.Log("drawn");
        //Universal Draw Code Here
    }
}