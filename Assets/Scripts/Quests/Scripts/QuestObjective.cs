using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Objective", order = 1)]
public class QuestObjective : ScriptableObject
{
    [Header("Objective Info")]
    public Quest quest;
    public string title;
    [TextArea(3, 10)]
    public string description;

    public bool showObjective = true;
    Object questMarkerPosition;

    [Header("Progress")]
    public int requiredAmount = 1;
    public int currentAmountReset = 0;
    public int currentAmountCheckpoint = 0;
    public int currentAmount = 0;

    public bool canCompleteReset = false;
    public bool canCompleteCheckpoint = false;
    public bool canComplete = false;

    public bool completedReset;
    public bool completedCheckpoint;
    public bool completed;


    private void Awake()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        completed = completedReset;
        currentAmount = currentAmountReset;
        canComplete = canCompleteReset;

        completedCheckpoint = completedReset;
        currentAmountCheckpoint = currentAmountReset;
        canCompleteCheckpoint = canCompleteReset;
    }

    public void SaveProgress()
    {
        completedCheckpoint = completed;
        currentAmountCheckpoint = currentAmount;
        canCompleteCheckpoint = canComplete;
    }

    public void LoadProgress()
    {
        completed = completedCheckpoint;
        currentAmount = currentAmountCheckpoint;
        canComplete = canCompleteCheckpoint;
    }

    public void SetCanComplete()
    {
        canComplete = true;
        SaveProgress();
        //Debug.Log("Current Objective: " + title);

        //ShowMarkers()
    }

    public void ShowMarkers()
    {
        GameObject objects = GameObject.Find(questMarkerPosition.name);

        if (objects != null)
        {
            //spawn quest marker on object
        }
    }

    public void CompleteGoal()
    {
        if (canComplete)
        {
            currentAmount++;

            if (currentAmount >= requiredAmount)
            {
                CompleteObjective();
            }
        }

        SaveProgress();
    }

    void CompleteObjective()
    {
        completed = true;
        
        if (quest != null)
        {
            quest.CompleteObjective(this);
            quest.CheckObjectives();
        }

        SaveProgress();

        quest.ResetCompass();
    }
}
