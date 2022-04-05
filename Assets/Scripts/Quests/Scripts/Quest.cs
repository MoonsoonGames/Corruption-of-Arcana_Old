using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest", order = 0)]
public class Quest : ScriptableObject
{
    #region Setup

    #region Variables

    [Header("Quest Info")]
    public string title;
    public int questNumber;
    [TextArea(3, 10)]
    public string description;
    public string questGiverName;
    public Sprite objectiveSprite;
    public Sprite objectiveLocation;

    [Header("Quest Objective Rules")]
    public bool showAllObjectives;
    public bool showFinalObjective;
    public QuestObjective[] objectives;

    [Header("Rewards")]
    public int goldReward;

    //delete
    public int potionReward;
    public int itemReward;

    [Header("Progress")]
    public QuestObjective currentObjective;

    public bool isActiveReset;
    public bool isActive;
    public bool isRevealled;
    public bool isRevealledReset;

    public bool isCompleteReset;
    public bool isComplete;

    #endregion

    #region Saving and Loading

    private void Awake()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        isActive = isActiveReset;
        isComplete = isCompleteReset;
        isRevealled = isRevealledReset;
    }

    #endregion

    #endregion

    #region Progress

    public void AcceptQuest(string questName)
    {
        if (isComplete == false)
        {
            if (isActive == false)
            {
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

            //Debug.Log("Accepted Quest: " + title);
            isRevealled = true;
            isActive = true;

            questGiverName = questName;

            currentObjective = objectives[0];

            CheckCompletedObjectives();
        }

        //SaveProgress();
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

        //SaveProgress();

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

        //SaveProgress();

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
                        currentObjective = nextObjective;
                    }
                }
            }
        }

        //SaveProgress();

        CheckObjectives();
    }

    #endregion

    #region Helper Functions

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