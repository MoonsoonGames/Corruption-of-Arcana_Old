using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Objective", order = 1)]
public class QuestObjective : ScriptableObject
{
    public bool completedReset;
    public bool completed;
    public Quest quest;
    public string title;
    public string description;

    public bool showObjective = true;
    Object questMarkerPosition;

    public int requiredAmount = 1;
    public int currentAmountReset = 0;
    public int currentAmount = 0;

    public bool canCompleteReset = false;
    public bool canComplete = false;

    private void Awake()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        completed = completedReset;
        currentAmount = currentAmountReset;
        canComplete = canCompleteReset;
    }

    public void SetCanComplete()
    {
        canComplete = true;
        Debug.Log("Current Objective: " + title);

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
    }

    void CompleteObjective()
    {
        completed = true;
        
        if (quest != null)
        {
            quest.CompleteObjective(this);
            quest.CheckObjectives();
        }
    }
}
