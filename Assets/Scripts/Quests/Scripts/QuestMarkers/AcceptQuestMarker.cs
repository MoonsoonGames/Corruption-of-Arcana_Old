using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptQuestMarker : MonoBehaviour
{
    public Quest[] quests;

    public QuestMarkers marker;

    private void Start()
    {
        CheckObjective();
    }

    public void CheckObjective()
    {
        marker.showMarker = false;

        if (CheckQuestsCompleted() && CheckQuestsInProgress())
        {
            if (marker != null)
            {
                bool contains = false;

                foreach (var item in quests)
                {
                    if (item.isRevealled == false && item.isComplete == false)
                    {
                        Debug.Log(item.title);
                        contains = true;
                    }
                }

                marker.showMarker = contains;
            }
        }
    }

    #region Quest Progress Requirements

    public Quest[] requireQuestsInProgress;
    public QuestObjective[] requireObjectivesInProgress;
    public bool requireAllInProgress = true;

    public Quest[] disableQuestsInProgress;
    public QuestObjective[] disableObjectivesInProgress;

    public bool CheckQuestsInProgress()
    {
        bool enableNode = false;

        bool contains1 = false;
        bool containsAll = true;

        foreach (var item in requireQuestsInProgress)
        {
            if (item.isRevealled)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        foreach (var item in requireObjectivesInProgress)
        {
            if (item.canComplete)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        enableNode = (containsAll) || (!requireAllCompleted && contains1);

        foreach (var item in disableQuestsInProgress)
        {
            if (item.isActive)
            {
                enableNode = false;
            }
        }

        foreach (var item in disableObjectivesInProgress)
        {
            if (item.canComplete)
            {
                enableNode = false;
            }
        }

        return enableNode;
    }

    #endregion

    #region Quest Completed Requirements

    public Quest[] requireQuestsCompleted;
    public QuestObjective[] requireObjectivesCompleted;
    public bool requireAllCompleted = true;

    public Quest[] disableQuestsCompleted;
    public QuestObjective[] disableObjectivesCompleted;

    public bool CheckQuestsCompleted()
    {
        bool enableNode = false;

        bool contains1 = false;
        bool containsAll = true;

        foreach (var item in requireQuestsCompleted)
        {
            if (item.isComplete)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        foreach (var item in requireObjectivesCompleted)
        {
            if (item.completed)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        enableNode = (containsAll) || (!requireAllCompleted && contains1);

        foreach (var item in disableQuestsCompleted)
        {
            if (item.isComplete)
            {
                enableNode = false;
            }
        }

        foreach (var item in disableObjectivesCompleted)
        {
            if (item.completed)
            {
                enableNode = false;
            }
        }

        return enableNode;
    }

    #endregion
}
