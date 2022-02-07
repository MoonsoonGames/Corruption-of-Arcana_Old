using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowQuestMarker : MonoBehaviour
{
    public QuestObjective objective;

    public GameObject marker;

    private void Start()
    {
        ShowObjective();
    }

    public void ShowObjective()
    {
        if (marker != null)
        {
            marker.SetActive(objective.canComplete && !objective.completed);
        }
    }
}
