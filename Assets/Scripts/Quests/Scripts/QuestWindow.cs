using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text goldRewardText;
    public Text potionRewardText;
    public Text itemRewardText;

    public void OpenQuestWindow(string title, string description, string gold, string potions, string item)
    {
        titleText.text = title;
        descriptionText.text = description;
        goldRewardText.text = gold;
        potionRewardText.text = potions;
        itemRewardText.text = item;
    }
}
