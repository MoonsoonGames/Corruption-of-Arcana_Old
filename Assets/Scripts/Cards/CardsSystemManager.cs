using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsSystemManager : MonoBehaviour
{
    public DeckManager[] decks;

    private DeckManager dealerDeck, playAreaDeck, navigationDeck, accumulator, enemyActionsDeck, enemyEffectsDeck, burnDeck, playerCharacterDeck, playerHandDeck, playerActionsDeck, playerEffectsDeck;
    public E_Decks Dealer, PlayArea, Navigation, Accumulator, EnemyActions, EnemyEffects, Burn, PlayerCharacter, PlayerHand, PlayerActions, PlayerEffects, Beast, Bandit, Elf, Dragon, Items, Weapons, Artefacts, Consumables, Actions, BasicActions, WeaponActions, SpellActions, Boons, Banes;

    private void Awake()
    {
        foreach (var item in decks)
        {
            if (item != null)
            {
                item.Setup();

                E_Decks typeCompare = item.deck.deckType;

                #region Set up private decks

                if (typeCompare == Dealer)
                {
                    dealerDeck = item;
                }
                else if (typeCompare == PlayArea)
                {
                    playAreaDeck = item;
                }
                else if (typeCompare == Navigation)
                {
                    navigationDeck = item;
                }
                else if (typeCompare == Accumulator)
                {
                    accumulator = item;
                }
                else if (typeCompare == EnemyActions)
                {
                    enemyActionsDeck = item;
                }
                else if (typeCompare == EnemyEffects)
                {
                    enemyEffectsDeck = item;
                }
                else if (typeCompare == Burn)
                {
                    burnDeck = item;
                }
                else if (typeCompare == PlayerCharacter)
                {
                    playerCharacterDeck = item;
                }
                else if (typeCompare == PlayerHand)
                {
                    playerHandDeck = item;
                }
                else if (typeCompare == PlayerActions)
                {
                    playerActionsDeck = item;
                }
                else if (typeCompare == PlayerEffects)
                {
                    playerEffectsDeck = item;
                }

                #endregion
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dealerDeck != null && accumulator != null)
        {
            dealerDeck.DrawCards(1, accumulator.deck, typeof(Destiny));
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && dealerDeck != null && accumulator != null)
        {
            dealerDeck.Shuffle(1000);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && dealerDeck != null && accumulator != null)
        {
            accumulator.TransferDeck(dealerDeck.deck, typeof(Destiny));
        }
    }
}
