using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateQuestInfo : MonoBehaviour
{
    QuestButtons[] questButtons;

    private void Start()
    {
        Setup();
    }

    private void Awake()
    {
        Setup();
    }

    void Setup()
    {
        questButtons = GetComponentsInChildren<QuestButtons>();
        UpdateQuestInfo();
    }

    public void UpdateQuestInfo()
    {
        foreach(var item in questButtons)
        {
            item.GenerateQuestInfo();
        }
    }
}
