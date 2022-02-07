using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetQuest : MonoBehaviour
{
    Quest[] quests;
    QuestObjective[] objectives;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Reset");
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
