using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class SpawnDialogue : MonoBehaviour
{
    public GameObject[] flowCharts;
    public GameObject backupFlowChart;
    LoadSettings loadSettings;

    public Color singleBGColour;
    public Color multipleBGColour;
    public Image background;

    private void Start()
    {
        if (LoadSettings.instance != null)
        {
            loadSettings = LoadSettings.instance;
        }

        Object flowChart = loadSettings.dialogueFlowChart;

        SetBGColour();
        BeginDialogue(flowChart);

        loadSettings.dialogueFlowChart = null;
    }

    void SetBGColour()
    {
        if (loadSettings != null && loadSettings.loadSceneMultiple)
        {
            background.color = multipleBGColour;
        }
        else
        {
            background.color = singleBGColour;
        }
    }

    void BeginDialogue(Object flowChart)
    {
        bool spawned = false;

        if (flowChart != null)
        {
            foreach (var item in flowCharts)
            {
                if (item.name == flowChart.name)
                {
                    item.SetActive(true);
                    spawned = true;
                }
            }
        }
        
        if (spawned == false && backupFlowChart != null)
        {
            backupFlowChart.SetActive(true);
        }
    }
}