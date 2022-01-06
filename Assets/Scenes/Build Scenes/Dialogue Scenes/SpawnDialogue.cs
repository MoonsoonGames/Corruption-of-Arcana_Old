using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SpawnDialogue : MonoBehaviour
{
    public GameObject[] flowCharts;
    LoadSettings loadSettings;

    private void Start()
    {
        LoadSettings[] loadSettingsArray = GameObject.FindObjectsOfType<LoadSettings>();

        foreach (var item in loadSettingsArray)
        {
            if (item.CheckMain())
            {
                loadSettings = item;
            }
            else
            {
                Destroy(item); //There is already one in the scene, delete this one
            }

        }
        
        BeginDialogue(loadSettings.dialogueFlowChart);

        loadSettings.dialogueFlowChart = null;
    }

    public void BeginDialogue(Object flowChart)
    {
        foreach (var item in flowCharts)
        {
            if (item.name == flowChart.name)
            {
                item.SetActive(true);
            }
        }
    }
}
