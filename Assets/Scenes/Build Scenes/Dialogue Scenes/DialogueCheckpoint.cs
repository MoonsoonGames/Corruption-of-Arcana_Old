using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueCheckpoint : MonoBehaviour
{
    LoadSettings loadSettings;

    private void Start()
    {
        loadSettings = LoadSettings.instance;
    }

    public void CheckpointDialogue()
    {
        if (loadSettings != null)
        {
            loadSettings.SetCheckpoint();
        }
        else
        {
            Debug.LogError("No load settings");
        }
    }

    public void GoldFromCards(Flowchart flowchart)
    {
        int gold = 0;

        if (loadSettings != null)
        {
            gold = loadSettings.DetermineGoldFromCards();

            loadSettings.ResetCards(true);
        }
        else
        {
            Debug.LogError("No load settings");
        }

        Debug.Log("Gold gained from cards should be " + gold);

        if (flowchart == null)
        {
            Debug.Log("no flow chart");
        }

        Debug.Log(flowchart.GetIntegerVariable("Gold"));
        flowchart.SetIntegerVariable("Gold", gold);
        Debug.Log(flowchart.GetIntegerVariable("Gold"));
    }
}
