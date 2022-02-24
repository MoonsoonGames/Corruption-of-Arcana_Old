using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenuHandler : MonoBehaviour
{
    Quest questScript;
    Quest[] quests;
    public Sprite CompleteIcon;
    public Sprite InProgressIcon;
    public Sprite NotFoundIcon;

    public Text QuestName;
    public Text QuestStatus;
    public Text QuestDesc;
    public Text QuestObjName;
    public Text QuestObj;
    public Image QuestStatusImg;

    public string PotionReward; //remove upon trader workings
    public string GoldReward;

    public Text QuestButtonNaming;
    public Image QuestGiver;
    public Image QuestGiverLocation;


    public void Start()
    {
        quests = Resources.FindObjectsOfTypeAll<Quest>();
    }

    public void Update()
    {
        foreach(Quest quests in quests)
        {
            if (quests.isActive == true || quests.isComplete == true)
            {
                QuestButtonNaming.text = quests.title;
                if (quests.isActive == true)
                {
                    GetComponent<Image>().sprite = InProgressIcon;
                }
                else
                {
                    GetComponent<Image>().sprite = CompleteIcon;
                }
            }
            else
            {
                QuestButtonNaming.text = "?????";
                GetComponent<Image>().sprite = NotFoundIcon;
            }
        }
    }

    public void QuestButton()
    {
        foreach (Quest quests in quests)
        {
            switch (quests.questNumber)
            {
                #region Quest 0
                case 0:
                    #region QuestBoard
                    //SET QuestName
                    QuestName.text = quests.title;
                    //SET QuestStatus
                    if (questScript.isActive == true)
                    {
                        QuestStatus.text = "In Progress";
                    }
                    else if (questScript.isComplete == true)
                    {
                        QuestStatus.text = "Completed";
                    }
                    else
                    {
                        QuestStatus.text = "Not Found";
                    }
                    //SET QuestDesc
                    QuestDesc.text = quests.description;
                    //SET QuestObjName
                    //SET QuestObj

                    //SET Potionrewards
                    //PotionReward = quests.potionReward.ToString();
                    //SET gold rewards
                    //GoldReward = quests.goldReward.ToString();
                    #endregion

                    #region ArtWork
                    //SET quest giver sprite
                    //SET quest giver location art
                    #endregion
                    break;
                #endregion

                #region Quest 1
                case 1:
                    #region QuestBoard
                    //SET QuestName
                    QuestName.text = questScript.title;
                    //SET QuestStatus
                    if (questScript.isActive == true)
                    {
                        QuestStatus.text = "In Progress";
                    }
                    else if (questScript.isComplete == true)
                    {
                        QuestStatus.text = "Completed";
                    }
                    else
                    {
                        QuestStatus.text = "Not Found";
                    }
                    //SET QuestDesc
                    QuestDesc.text = questScript.description;
                    //SET QuestObjName
                    //SET QuestObj

                    //SET Potionrewards
                    PotionReward = questScript.potionReward.ToString();
                    //SET gold rewards
                    GoldReward = questScript.goldReward.ToString();
                    #endregion

                    #region ArtWork
                    //SET quest giver sprite
                    //SET quest giver location art
                    #endregion
                    break;
                #endregion

                #region Quest 2
                case 2:
                    #region QuestBoard
                    //SET QuestName
                    QuestName.text = questScript.title;
                    //SET QuestStatus
                    if (questScript.isActive == true)
                    {
                        QuestStatus.text = "In Progress";
                    }
                    else if (questScript.isComplete == true)
                    {
                        QuestStatus.text = "Completed";
                    }
                    else
                    {
                        QuestStatus.text = "Not Found";
                    }
                    //SET QuestDesc
                    QuestDesc.text = questScript.description;
                    //SET QuestObjName
                    //SET QuestObj

                    //SET Potionrewards
                    PotionReward = questScript.potionReward.ToString();
                    //SET gold rewards
                    GoldReward = questScript.goldReward.ToString();
                    #endregion

                    #region ArtWork
                    //SET quest giver sprite
                    //SET quest giver location art
                    #endregion
                    break;
                #endregion

                #region Quest 3
                case 3:
                    #region QuestBoard
                    //SET QuestName
                    QuestName.text = questScript.title;
                    //SET QuestStatus
                    if (questScript.isActive == true)
                    {
                        QuestStatus.text = "In Progress";
                    }
                    else if (questScript.isComplete == true)
                    {
                        QuestStatus.text = "Completed";
                    }
                    else
                    {
                        QuestStatus.text = "Not Found";
                    }
                    //SET QuestDesc
                    QuestDesc.text = questScript.description;
                    //SET QuestObjName
                    //SET QuestObj

                    //SET Potionrewards
                    PotionReward = questScript.potionReward.ToString();
                    //SET gold rewards
                    GoldReward = questScript.goldReward.ToString();
                    #endregion

                    #region ArtWork
                    //SET quest giver sprite
                    //SET quest giver location art
                    #endregion
                    break;
                    #endregion
            }
        }
    }
}
