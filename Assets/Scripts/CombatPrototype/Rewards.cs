using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    public CombatManager combatManager;

    public void GiveRewards()
    {
        foreach (var item in LoadSettings.instance.currentFightObjectives)
        {
            item.CompleteGoal();
        }

        LoadSettings.instance.currentGold += (int)Random.Range(LoadSettings.instance.goldReward.x, LoadSettings.instance.goldReward.y);
        LoadSettings.instance.AddWeapon(LoadSettings.instance.rewardWeapon);

        LoadSettings.instance.currentFightObjectives.Clear();

        if (combatManager != null)
        {
            combatManager.Rewards(15);
        }
    }

    public void Defeated()
    {
        LoadSettings.instance.currentFightObjectives.Clear();
        LoadSettings.instance.rewardWeapon = null;
        LoadSettings.instance.goldReward = new Vector2(0, 0);
    }
}