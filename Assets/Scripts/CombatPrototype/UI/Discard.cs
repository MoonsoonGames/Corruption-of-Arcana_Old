using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard : MonoBehaviour
{
    AbilityManager abilityManager;

    public void DiscardCard()
    {
        if (abilityManager == null)
        {
            abilityManager = GameObject.FindObjectOfType<AbilityManager>();
        }

        if (abilityManager != null)
        {
            abilityManager.DiscardCard();
        }
    }
}
