using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest", order = 0)]
public class Quest : ScriptableObject
{
    public bool isActiveReset;
    public bool isActive;

    public bool isCompleteReset;
    public bool isComplete;

    private void Awake()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        isActive = isActiveReset;
        isComplete = isCompleteReset;
    }

    [Header("Quest Info")]
    public string title;
    public string description;
    public bool showAllObjectives;
    public bool showFinalObjective;
    public QuestObjective[] objectives;

    [Header("Rewards")]
    public int goldReward;
    public int potionReward;
    public string itemReward;

    public void AcceptQuest()
    {
        //Debug.Log("Accepted Quest: " + title);
        isActive = true;

        if (showAllObjectives)
        {
            for (int i = 0; i < objectives.Length; i++)
            {
                if (i == objectives.Length - 1)
                {
                    if (showFinalObjective)
                        objectives[i].SetCanComplete();
                }
                else
                {
                    objectives[i].SetCanComplete();
                }
            }
        }
        else
        {
            objectives[0].canComplete = true;
        }
    }

    public void CheckObjectives()
    {
        bool allComplete = true;

        foreach (var item in objectives)
        {
            if (item.completed == false)
            {
                Debug.Log(item.title + " is " + item.completed);
                allComplete = false;
            }
        }

        if (allComplete)
        {
            //Debug.Log("Completed Quest: " + title);
            isComplete = true;
        }
        else
        {
            //Debug.Log("Not completed all objectives");
        }
    }

    public void CompleteObjective(QuestObjective objective)
    {
        //Debug.Log("Completed Objective: " + objective.title);

        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i] == objective)
            {
                if (!(i >= objectives.Length - 1))
                {
                    QuestObjective nextObjective = objectives[i + 1];

                    if (nextObjective != null)
                    {
                        nextObjective.SetCanComplete();
                    }
                }
            }
        }

        CheckObjectives();
    }

    public void ResetCompass()
    {
        Compass compass = GameObject.FindObjectOfType<Compass>();

        if (compass != null)
        {
            compass.ResetMarkersOnce();
        }
    }
}