using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRewards : MonoBehaviour
{
    LoadSettings loadSettings;

    public Vector2Int goldReward;
    public float potionReward;

    private void Start()
    {
        loadSettings = LoadSettings.instance;
    }

    public void GiveRewards()
    {
        Debug.Log("Give rewards");
        if (loadSettings != null && loadSettings.currentFight != null)
        {
            loadSettings.currentGold += (int)Random.Range(goldReward.x, goldReward.y);
            loadSettings.potionCount = DeterminePotions(loadSettings.potionCount);
        }
    }

    int DeterminePotions(float potions)
    {
        int potionsReward = (int)potions;
        float chance = potions % 1f;

        int test = potionsReward;

        if (RandomBoolWeighting(chance))
            potionsReward++;

        //Debug.Log(test + " | " + chance + " | " + potionsReward);

        return Mathf.Clamp(potionsReward, 0, 5);
    }

    //From Gam140 Godsent by Andrew Scott
    private bool RandomBoolWeighting(float weighting)
    {
        if (Random.value >= weighting)
        {
            return true;
        }
        return false;
    }
}
