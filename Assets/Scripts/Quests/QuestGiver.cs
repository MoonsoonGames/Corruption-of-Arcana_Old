using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public GameObject questWindow;

    public void QuestWindow(bool open)
    {
        if (quest != null && questWindow != null)
        {
            questWindow.SetActive(open);
            //questWindow.GetComponent<QuestWindow>().OpenWindow(/*quest info here*/)
            //Set info in quest window to the quest info
        }
        else
        {
            Debug.Log("No valid quest to set");
        }
    }

    public void AcceptQuest()
    {
        //Add to guidebook
        //Set window to false
        //Check Objectives
        quest.AcceptQuest();

        LoadSettings loadSettings = GameObject.FindObjectOfType<LoadSettings>();

        loadSettings.quests.Add(quest);
    }
}
