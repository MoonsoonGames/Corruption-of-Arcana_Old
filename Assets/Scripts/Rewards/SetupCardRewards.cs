using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardRewards : MonoBehaviour
{
    CardSetter[] cardSetters;

    public CardParent[] cards;

    public ActiveCard activeCard;

    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        cardSetters = GetComponentsInChildren<CardSetter>();
        sceneLoader = GetComponent<SceneLoader>();

        foreach (var item in cardSetters)
        {
            item.Setup(DetermineCard());
        }
    }

    CardParent DetermineCard()
    {
        int rInt = Random.Range(0, cards.Length - 1);
        //Debug.Log(cards[rInt].cardName);
        return cards[rInt];
    }

    public void ConfirmCardChoice(CardSetter cardSetter)
    {
        CardParent card = cardSetter.GetCard();

        //Add card to load settings
        LoadSettings loadSettings = LoadSettings.instance;

        loadSettings.majourArcana.Add(card);

        //Load last scene
        if (sceneLoader != null)
        {
            sceneLoader.LoadLastScene(null);
        }
        else
        {
            Debug.LogError("No scene loader");
        }
    }

    #region Card Preview

    public void ShowCard(CardSetter cardSetter)
    {
        CardParent ability = cardSetter.GetCard();

        if (activeCard != null)
        {
            if (ability.selfInterpretationUnlocked && ability.targetInterpretationUnlocked)
            {
                activeCard.ReadyCard(ability.cardName, "Two interpretations", ability.selfHeal, "Unknown", ability.selfCost, "Two interpretations active, UI issue", ability.selfCostType);
            }
            else if (ability.selfInterpretationUnlocked)
            {
                activeCard.ReadyCard(ability.cardName, ability.selfName, ability.RestoreValue(), ability.RestoreType(), ability.selfCost, ability.selfDescription, ability.selfCostType);
            }
            else if (ability.targetInterpretationUnlocked)
            {
                activeCard.ReadyCard(ability.cardName, ability.targetName, ability.TotalDmgRange(), ability.damageType.ToString(), ability.targetCost, ability.targetDescription, ability.targetCostType);
            }
        }
    }

    #endregion
}
