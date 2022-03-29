using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadQuestData : MonoBehaviour
{
    Quest[] quests;
    QuestObjective[] objectives;

    LoadSettings loadSettings;
    //saved data that gets reset


    private void Start()
    {
        quests = Resources.FindObjectsOfTypeAll<Quest>();
        objectives = Resources.FindObjectsOfTypeAll<QuestObjective>();

        loadSettings = GetComponent<LoadSettings>();
    }
}
