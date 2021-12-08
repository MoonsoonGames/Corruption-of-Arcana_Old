using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 When adding a new value for any enums,
always put it at the end of the list. 
Otherwise, you will have to reassign
all references that go after the list!!!

DO NOT ADD ANY ENUMS AT THE START OF THE LIST!!!
*/

public enum E_Levels
{
    [InspectorName("Main Menu")]
    SplashScreen,
    [InspectorName("Thoth")]
    Thoth,
    [InspectorName("Combat")]
    CombatPrototype,

    Narrative,
    Ending,
    Fortune,
    Death,

    [InspectorName("Overworld Map")]
    OverworldMap,

    [InspectorName("Clearing")]
    Clearing,

    [InspectorName("Checkpoint")]
    Checkpoint
}

public enum E_Suits
{
    [InspectorName("Fortune")]
    Fortune,
    [InspectorName("Harmony")]
    Harmony,
    [InspectorName("Misfortune")]
    Misfortune
}

public enum E_Decks
{
    //Owned by Dealer
    [InspectorName("Dealer")]
    Dealer,
    [InspectorName("Play Area")]
    PlayArea,
    [InspectorName("Navigation")]
    Navigation,
    [InspectorName("Accumulator")]
    Accumulator,
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
    PlayerEffects,

    //Events
    [InspectorName("Beast")]
    Beast,
    [InspectorName("Bandit")]
    Bandit,
    [InspectorName("Elf")]
    Elf,
    [InspectorName("Dragon")]
    Dragon,

    //Items
    [InspectorName("Items")]
    Items,
    [InspectorName("Weapons")]
    Weapons,
    [InspectorName("Artefacts")]
    Artefacts,
    [InspectorName("Consumables")]
    Consumables,

    //Actions
    [InspectorName("Actions")]
    Actions,
    [InspectorName("Basic Actions")]
    BasicActions,
    [InspectorName("Weapon Actions")]
    WeaponActions,
    [InspectorName("Spell Actions")]
    SpellActions,

    //Effects
    [InspectorName("Boons")]
    Boons,
    [InspectorName("Banes")]
    Banes
}

public enum E_CombatCards
{
    Slash, Cleave, Flurry, ChillTouch, Firebolt, ChainLightning, CureWounds, CriticalSlash, HealingPotion, StormBarrage
}