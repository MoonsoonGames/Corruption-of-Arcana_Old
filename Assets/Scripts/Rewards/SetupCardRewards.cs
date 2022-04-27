using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupCardRewards : MonoBehaviour
{
    CardSetter[] cardSetters;

    public CardParent[] cards;

    public ActiveCard activeCard;

    CardParent cardChoice;

    SceneLoader sceneLoader;

    bool canChoose = true;

    public GameObject confirmationMenu;
    public Text confirmText;

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

    public void SelectCardChoice(CardSetter cardSetter)
    {
        if (canChoose)
        {
            cardChoice = cardSetter.GetCard();
            SetConfirmationMenu(true);
        }
    }

    public void ConfirmCardChoice()
    {
        if (canChoose && cardChoice != null)
        {
            canChoose = false;

            //Add card to load settings
            LoadSettings.instance.majourArcana.Add(cardChoice);
            SetConfirmationMenu(false);
        }
    }

    public void SetConfirmationMenu(bool active)
    {
        if (confirmationMenu != null)
        {
            confirmationMenu.SetActive(active);
        }

        if (confirmText != null)
        {
            if (cardChoice != null)
                confirmText.text = "Are you sure you want to select the " + cardChoice.cardName + " card?";
            else
                confirmText.text = "Are you sure you want to select this card?";
        }
    }

    public void LoadScene(bool loadCheckpointScene)
    {
        //Load last scene
        if (sceneLoader != null)
        {
            if (loadCheckpointScene)
            {
                sceneLoader.LoadCheckpointScene(null);
            }
            else
            {
                sceneLoader.LoadLastScene(null);
            }
        }
        else
        {
            Debug.LogError("No scene loader");
            gameObject.SetActive(false);
        }
    }

    #region Card Preview

    public void ShowCard(CardSetter cardSetter)
    {
        Debug.Log("Hover");
        CardParent ability = cardSetter.GetCard();

        if (activeCard != null)
        {
            if (ability.selfInterpretationUnlocked && ability.targetInterpretationUnlocked)
            {
                activeCard.ReadyCard(ability.cardName, ability.comboCard != null ? ability.comboCard.cardName : "None", ability.selfHeal, "Unknown", ability.selfCost, "Two interpretations active, UI issue", ability.selfCostType);
            }
            else if (ability.selfInterpretationUnlocked)
            {
                activeCard.ReadyCard(ability.cardName, ability.comboCard != null ? ability.comboCard.cardName : "None", ability.RestoreValue(), ability.RestoreType(), ability.selfCost, ability.selfDescription, ability.selfCostType);
            }
            else if (ability.targetInterpretationUnlocked)
            {
                activeCard.ReadyCard(ability.cardName, ability.comboCard != null ? ability.comboCard.cardName : "None", ability.TotalDmgRange(), ability.damageType.ToString(), ability.targetCost, ability.targetDescription, ability.targetCostType);
            }
        }
    }

    #endregion
}
