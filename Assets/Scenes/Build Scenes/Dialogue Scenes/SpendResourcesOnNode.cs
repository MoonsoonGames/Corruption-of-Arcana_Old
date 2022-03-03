using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SpendResourcesOnNode : MonoBehaviour
{
    LoadSettings loadSettings;

    private void Start()
    {
        loadSettings = LoadSettings.instance;
    }

    public void SpendGold(Flowchart flowchart)
    {
        if (loadSettings != null)
        {
            int cost = flowchart.GetIntegerVariable("Cost");

            loadSettings.currentGold -= cost;
        }
        else
        {
            Debug.LogError("No load settings!");
        }
    }

    public void HasGold(Flowchart flowchart)
    {
        if (loadSettings != null)
        {
            int cost = flowchart.GetIntegerVariable("BribeCost");

            flowchart.SetBooleanVariable("CanBribe", true);

            if (loadSettings.currentGold >= cost)
            {
                //Has the gold
            }
            else if (loadSettings.currentGold == 0)
            {
                flowchart.SetBooleanVariable("CanBribe", false);
            }
            else
            {
                //Insufficient gold
                flowchart.SetIntegerVariable("BribeCost", loadSettings.currentGold);
            }
        }
        else
        {
            Debug.LogError("No load settings!");
        }
    }

    public void GetPlayergold(Flowchart flowchart)
    {
        if (loadSettings != null)
        {
            flowchart.SetIntegerVariable("PlayerGold", loadSettings.currentGold);
        }
        else
        {
            Debug.LogError("No load settings!");
        }
    }

    public void GetIncreaseMaxHPCost(Flowchart flowchart)
    {
        if (loadSettings != null)
        {
            flowchart.SetIntegerVariable("IncreaseHealthCost", loadSettings.GetHeathIncreaseCost());
        }
        else
        {
            Debug.LogError("No load settings!");
        }
    }
}
