using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Objective", order = 1)]
public class QuestObjective : ScriptableObject
{
    [Header("Objective Info")]
    public Quest quest;
    public string title;
    public Sprite objectiveSprite;
    public Sprite locationSprite;
    [TextArea(3, 10)]
    public string description;

    public bool showObjective = true;
    Object questMarkerPosition;

    [Header("Progress")]
    public int requiredAmount = 1;
    public int currentAmountReset = 0;
    public int currentAmount = 0;

    public bool canCompleteReset = false;
    public bool canComplete = false;

    public bool completedReset;
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
    }

    public void SetCanComplete()
    {
        canComplete = true;
        quest.objectiveLocation = locationSprite;
        quest.objectiveSprite = objectiveSprite;
        quest.currentObjective = this;
        //SaveProgress();
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
            Debug.Log("Complete");
            currentAmount++;

            if (currentAmount >= requiredAmount)
            {
                CompleteObjective();
            }
        }

        //SaveProgress();
    }

    void CompleteObjective()
    {
        completed = true;
        
        if (quest != null)
        {
            quest.CompleteObjective(this);
            quest.CheckObjectives();
        }

        //SaveProgress();

        quest.ResetCompass();
    }
}
