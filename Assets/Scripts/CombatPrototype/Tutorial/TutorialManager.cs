using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    int currentSpellIndex;
    public CardParent[] spells;
    [TextArea(3, 10)]
    public string[] messages;
    [TextArea(3, 10)]
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
        if (currentSpellIndex < afterCastMessages.Length)
        {
            tutorialMessages.ShowMessage(null, afterCastMessages[currentSpellIndex]);
            currentSpellIndex++;
        }
    }
}
