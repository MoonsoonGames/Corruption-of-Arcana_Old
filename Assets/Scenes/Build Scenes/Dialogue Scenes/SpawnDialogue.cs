using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class SpawnDialogue : MonoBehaviour
{
    public GameObject[] flowCharts;
    Flowchart[] flowChartArrays;
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

        SetBGColour();
        BeginDialogue(loadSettings.dialogueFlowChart);

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
        /*
        if (spawned! && backupFlowChart != null)
        {
            backupFlowChart.SetActive(true);
        }
        */
    }
}
