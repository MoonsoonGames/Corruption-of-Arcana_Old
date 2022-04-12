using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetQuest : MonoBehaviour
{
    Quest[] quests;
    QuestObjective[] objectives;

    public bool resetQuestData;

    // Start is called before the first frame update
    void Start()
    {
        if (resetQuestData)
        {
            quests = Resources.FindObjectsOfTypeAll<Quest>();
            objectives = Resources.FindObjectsOfTypeAll<QuestObjective>();

            foreach (var item in quests)
            {
                item.ResetValues();
            }
            foreach (var item in objectives)
            {
                item.ResetValues();
            }
        }
    }
}
