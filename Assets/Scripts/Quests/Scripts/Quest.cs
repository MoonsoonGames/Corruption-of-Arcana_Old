using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest", order = 0)]
public class Quest : ScriptableObject
{
    public bool isActiveReset;
    public bool isActiveCheckpoint;
    public bool isActive;

    public bool isCompleteReset;
    public bool isCompleteCheckpoint;
    public bool isComplete;

    private void Awake()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        isActive = isActiveReset;
        isComplete = isCompleteReset;

        isActiveCheckpoint = isActiveReset;
        isCompleteCheckpoint = isCompleteReset;
    }

    public void SaveProgress()
    {
        isActiveCheckpoint = isActive;
        isCompleteCheckpoint = isComplete;
    }

    public void LoadProgress()
    {
        isActive = isActiveCheckpoint;
        isComplete = isCompleteCheckpoint;
    }

    [Header("Quest Info")]
    public string title;
    public int questNumber;
    public string description;
    public bool showAllObjectives;
    public bool showFinalObjective;
    public QuestObjective[] objectives;

    [Header("Rewards")]
    public int goldReward;

    //delete
    public int potionReward;
    public int itemReward;

    public void AcceptQuest()
    {
        if (isComplete == false)
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

            CheckCompletedObjectives();
        }

        SaveProgress();
    }

    public void CheckObjectives()
    {
        bool allComplete = true;

        foreach (var item in objectives)
        {
            if (item.completed == false)
            {
                //Debug.Log(item.title + " is " + item.completed);
                allComplete = false;
            }
        }

        if (allComplete)
        {
            //Debug.Log("Completed Quest: " + title);
            isComplete = true;
            isActive = false;
            GiveRewards();
        }
        else
        {
            //Debug.Log("Not completed all objectives");
        }

        SaveProgress();

        ResetCompass();
    }

    public void CompleteObjective(QuestObjective objective)
    {
        //Debug.Log("Completed Objective: " + objective.title);

        objective.canComplete = false;

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

    public void CheckCompletedObjectives()
    {
        //Debug.Log("Completed Objective: " + objective.title);

        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].completed)
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
        AcceptQuestMarker[] acceptQuestMarkers = GameObject.FindObjectsOfType<AcceptQuestMarker>();

        foreach (var item in acceptQuestMarkers)
        {
            item.CheckObjective();
        }

        Compass compass = GameObject.FindObjectOfType<Compass>();

        if (compass != null)
        {
            compass.ResetMarkersOnce();
        }
    }

    #region Rewards

    void GiveRewards()
    {
        Debug.Log("Give rewards");
        LoadSettings loadSettings = LoadSettings.instance;

        if (loadSettings != null && loadSettings.currentFight != null)
        {
            loadSettings.currentGold += goldReward;
        }
    }

    #endregion
}