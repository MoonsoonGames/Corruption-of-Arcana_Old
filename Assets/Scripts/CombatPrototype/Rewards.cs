using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    LoadSettings loadSettings;

    public CombatManager combatManager;

    public void GiveRewards()
    {
        loadSettings = LoadSettings.instance;

        if (loadSettings.currentFightObjective != null)
        {
            loadSettings.currentFightObjective.CompleteGoal();
            loadSettings.currentFightObjective = null;
        }

        if (combatManager != null)
        {
            combatManager.Rewards(15);
        }
    }
}