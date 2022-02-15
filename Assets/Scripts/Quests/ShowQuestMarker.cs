using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowQuestMarker : MonoBehaviour
{
    public QuestObjective[] objectives;

    public QuestMarkers marker;

    private void Start()
    {
        CheckObjective();
    }

    public void CheckObjective()
    {
        if (marker != null)
        {
            bool contains = false;

            foreach (var item in objectives)
            {
                if (item.canComplete && item.completed == false && item.quest.isActive)
                {
                    Debug.Log(item.title);
                    contains = true;
                }
            }

            marker.showMarker = contains;
        }
    }
}
