using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    LoadSettings loadSettings;

    public GameObject[] images;

    public Text[] text;
    public Text[] count;

    public CombatManager combatManager;

    public void GiveRewards()
    {
        loadSettings = GameObject.FindObjectOfType<LoadSettings>();

        if (combatManager != null && loadSettings != null)
        {
            int gold = (int)Random.Range(loadSettings.goldReward.x, loadSettings.goldReward.y);

            int potions = DeterminePotions(loadSettings.potionReward);

            int healing = 15;

            string item = loadSettings.itemReward;

            if (gold > 0)
            {
                images[0].SetActive(true);

                text[0].text = "Gold";
                count[0].text = gold.ToString();
            }

            if (potions > 0)
            {
                images[1].SetActive(true);

                text[1].text = "Potions";
                count[1].text = potions.ToString();
            }

            if (healing > 0)
            {
                images[2].SetActive(true);

                text[2].text = "Health";
                count[2].text = healing.ToString();
            }

            if (item != "")
            {
                images[3].SetActive(true);

                text[3].text = item;
                count[3].text = "1";
            }

            combatManager.Rewards(healing, gold, potions);
        }
    }

    int DeterminePotions(float potions)
    {
        int potionsReward = (int)potions;
        float chance = potions % 1f;

        int test = potionsReward;

        if (RandomBoolWeighting(chance))
            potionsReward++;

        Debug.Log(test + " | " + chance + " | " + potionsReward);

        return potionsReward;
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
