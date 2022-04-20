using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    int currentSpellIndex;
    public CardParent[] spells;
    public string[] messages;
    public string[] afterCastMessages;
    public TutorialMessages tutorialMessages;

    public bool CanCastSpell(CardParent spell)
    {
        if (currentSpellIndex < spells.Length)
        {
            if (spells[currentSpellIndex] != spell)
            {
                tutorialMessages.ShowMessage(null, "Select the " + spells[currentSpellIndex].cardName + " card.");
                return false;
            }

            tutorialMessages.ShowMessage(null, messages[currentSpellIndex]);
        }

        return true;
    }

    public void CastSpell()
    {
        tutorialMessages.ShowMessage(null, afterCastMessages[currentSpellIndex]);
        currentSpellIndex++;
    }
}
