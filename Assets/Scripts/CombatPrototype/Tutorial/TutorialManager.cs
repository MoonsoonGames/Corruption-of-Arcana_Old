using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    int currentSpellIndex;
    public CardParent[] spells;
    public string[] messages;

    public bool CanCastSpell(CardParent spell)
    {
        if (currentSpellIndex < spells.Length)
        {
            if (spells[currentSpellIndex] != spell)
            {
                Debug.Log("Select the " + spells[currentSpellIndex].cardName + " card.");
                return false;
            }

            Debug.Log(messages[currentSpellIndex]);
        }

        return true;
    }

    public void CastSpell()
    {
        //remove messages
        currentSpellIndex++;
    }
}
