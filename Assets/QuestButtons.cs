using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButtons : MonoBehaviour
{
    public Sprite CompleteIcon;
    public Sprite InProgressIcon;
    public Sprite NotFoundIcon;

    public Text nameText;
    public Text statusText;
    public Text descriptionText;
    public Text objNameText;
    public Text objDescText;
    public Text objProgressCountText;
    public Text objProgressMaxText;
    public Text goldText;

    public Quest quest;

    public Text questName;
    public Image questStatus;

    public void Start()
    {
        questName.text = quest.title;

        if (quest.isComplete == true)
        {
            questStatus.sprite = CompleteIcon;
        }
        else if (quest.isRevealled == true)
        {
            questStatus.sprite = InProgressIcon;
        }
        else
        {
            questName.text = "?????";
            questStatus.sprite = NotFoundIcon;
            GetComponent<Button>().interactable = false;
        }
    }

    public void ButtonPressed()
    {
        if (nameText != null)
            nameText.text = quest.title;

        if (statusText != null)
        {
            if (quest.isComplete == true)
            {
                statusText.text = "Completed";

                objNameText.text = "You have finished this quest";
                objDescText.text = "You have finished this quest";
                /*
                objProgressCountText.text = quest.currentObjective.currentAmount.ToString();
                objProgressMaxText.text = quest.currentObjective.requiredAmount.ToString();
                */
            }
            else if (quest.isRevealled == true)
            {
                statusText.text = "In Progress";

                objNameText.text = quest.currentObjective.title;
                objDescText.text = quest.currentObjective.description;

                //objProgressCountText.text = quest.currentObjective.currentAmount.ToString();
                //objProgressMaxText.text = quest.currentObjective.requiredAmount.ToString();
            }
            else
            {
                statusText.text = "Not Started";

                objNameText.text = "You have yet to start this quest";
                objDescText.text = "You have yet to start this quest";

                //objProgressCountText.text = "0";
                //objProgressMaxText.text = "1";
            }
        }

        if (descriptionText != null)
            descriptionText.text = quest.description;

        //if (objNameText != null)
        //    objNameText.text = objname;

        //if (objDescText != null)
        //    objDescText.text = objdesc;

        if (goldText != null)
            goldText.text = quest.goldReward.ToString();

        /* 
        if (questGiver != null)
            questGiver.sprite = questgiver;

        if (questLocat != null)
            questLocat.sprite = questlocation;
        */
    }
}
