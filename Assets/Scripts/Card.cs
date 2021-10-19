using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    #region Setup

    private string cardName;
    private Deck currentDeck;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public string GetName()
    {
        return cardName;
    }

    public void SetName(string newCardName)
    {
        cardName = newCardName;
    }

    public Deck GetDeck()
    {
        return currentDeck;
    }

    public void SetDeck(Deck newDeck)
    {
        currentDeck = newDeck;
    }

    #endregion

    #region Drawing Cards

    public virtual void DrawToDeck(Deck newDeck)
    {
        currentDeck.RemoveCard(this, false);

        newDeck.AddCard(this);

        currentDeck = newDeck;

        OnDraw();
    }

    #endregion

    #region Card Actions

    public virtual void PlayCard()
    {

    }

    public virtual void OnDraw()
    {
        Debug.Log(currentDeck);
    }

    public virtual void CardRevealed(bool isRevealed)
    {

    }

    public virtual void CardUsed()
    {

    }

    public virtual void CardRemoved()
    {

    }

    public virtual void CardBurned()
    {

    }

    #region Pure/Corrupt Mechanic

    public virtual void CardPurified()
    {

    }

    public virtual void CardCorrupted()
    {

    }

    #endregion

    #endregion
}