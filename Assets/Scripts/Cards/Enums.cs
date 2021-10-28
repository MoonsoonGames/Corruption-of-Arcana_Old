using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Suits
{
    [InspectorName("Hearts - Fortune")]
    HeartsFortune,
    [InspectorName("Diamonds")]
    Diamonds,
    [InspectorName("Spades")]
    Spades,
    [InspectorName("Clubs - Misfortune")]
    ClubsMisfortune
}

public enum E_Decks
{
    //Owned by Dealer
    [InspectorName("Dealer")]
    Dealer,
    [InspectorName("Navigation")]
    Navigation,
    [InspectorName("Destiny")]
    Destiny,
    [InspectorName("Enemy Actions")]
    EnemyActions,
    [InspectorName("Enemy Effects")]
    EnemyEffects,
    [InspectorName("Burn")]
    Burn,

    //Owned by Player
    [InspectorName("Player Character")]
    PlayerCharacter,
    [InspectorName("Player Hand")]
    PlayerHand,
    [InspectorName("Player Actions")]
    PlayerActions,
    [InspectorName("Player Effects")]
    PlayerEffects
}