using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        SetBGColour();
        BeginDialogue(loadSettings.dialogueFlowChart);

        loadSettings.dialogueFlowChart = null;
    }

    void SetBGColour()
    {
        if (loadSettings.loadSceneMultiple)
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
