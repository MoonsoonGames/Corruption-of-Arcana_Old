using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadQuestData : MonoBehaviour
{
    Quest[] quests;
    QuestObjective[] objectives;

    private void Start()
    {
        quests = Resources.FindObjectsOfTypeAll<Quest>();
        objectives = Resources.FindObjectsOfTypeAll<QuestObjective>();
    }

    public void SaveQuestData()
    {
        foreach (var item in quests)
        {
            item.SaveProgress();
        }
        foreach (var item in objectives)
        {
            item.SaveProgress();
        }
    }

    public void LoadQuestData()
    {
        foreach (var item in quests)
        {
            item.LoadProgress();
        }
        foreach (var item in objectives)
        {
            item.LoadProgress();
        }
    }
}
