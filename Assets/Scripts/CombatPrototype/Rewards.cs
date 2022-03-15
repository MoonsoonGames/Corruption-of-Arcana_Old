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

        foreach (var item in loadSettings.currentFightObjectives)
        {
            item.CompleteGoal();
        }

        loadSettings.currentFightObjectives.Clear();

        if (combatManager != null)
        {
            combatManager.Rewards(15);
        }
    }
}