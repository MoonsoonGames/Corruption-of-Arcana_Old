using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMenuManager : MonoBehaviour
{
    public ActiveCard activeCard;

    public void ShowCardInfo(CardParent spell)
    {
        if (activeCard != null)
        {
            if (spell != null)
            {
                if (spell.selfInterpretationUnlocked && spell.targetInterpretationUnlocked)
                {
                    activeCard.ReadyCard(spell.cardName, "Two interpretations", spell.selfHeal, "Unknown", spell.selfCost, "Two interpretations active, UI issue", spell.selfCostType);
                }
                else if (spell.selfInterpretationUnlocked)
                {
                    activeCard.ReadyCard(spell.cardName, spell.selfName, spell.RestoreValue(), spell.RestoreType(), spell.selfCost, spell.selfDescription, spell.selfCostType);
                }
                else if (spell.targetInterpretationUnlocked)
                {
                    activeCard.ReadyCard(spell.cardName, spell.targetName, spell.TotalDmgRange(), spell.damageType.ToString(), spell.targetCost, spell.targetDescription, spell.targetCostType);
                }

                activeCard.gameObject.SetActive(true);
            }
            else
            {
                activeCard.gameObject.SetActive(false);
            }
        }
    }
}
