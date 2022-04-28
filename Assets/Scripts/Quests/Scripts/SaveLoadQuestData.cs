using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadQuestData : MonoBehaviour
{
    [HideInInspector]
    public Quest[] quests;
    [HideInInspector]
    public QuestObjective[] objectives;

    public void Setup()
    {
        quests = Resources.FindObjectsOfTypeAll<Quest>();
        objectives = Resources.FindObjectsOfTypeAll<QuestObjective>();
    }

    public void ResetQuestData()
    {
        foreach (var item in quests)
        {
            item.ResetValues();
        }
        foreach (var item in objectives)
        {
            item.ResetValues();
        }
    }

    public void Load()
    {
        SaveData data = SaveSystem.LoadSaveData();

        if (data != null)
        {
            foreach (Quest quest in quests)
            {
                Quest loadQuest = GetQuest(quest);

                quest.isActive = loadQuest.isActive;
                quest.isComplete = loadQuest.isComplete;
                quest.isRevealled = loadQuest.isRevealled;
                quest.currentObjective = loadQuest.currentObjective;
            }

            foreach (QuestObjective objective in objectives)
            {
                QuestObjective loadQuest = GetObjective(objective);

                objective.canComplete = loadQuest.canComplete;
                objective.completed = loadQuest.completed;
                objective.currentAmount = loadQuest.currentAmount;
            }
        }
        else
        {
            Debug.LogError("No quest data");
        }
    }

    public Quest GetQuest(Quest questRef)
    {
        foreach(Quest quest in quests)
        {
            if (quest == questRef)
            {
                return quest;
            }
        }

        return null;
    }

    public QuestObjective GetObjective(QuestObjective objectiveRef)
    {
        foreach (QuestObjective objective in objectives)
        {
            if (objective == objectiveRef)
            {
                return objective;
            }
        }

        return null;
    }

    public Quest GetQuestFromData(string questRef)
    {
        foreach (Quest quest in quests)
        {
            if (quest.name == questRef)
            {
                return quest;
            }
        }

        return null;
    }

    public QuestObjective GetObjectiveFromData(string objectiveRef)
    {
        foreach (QuestObjective objective in objectives)
        {
            if (objective.name == objectiveRef)
            {
                return objective;
            }
        }

        return null;
    }
}