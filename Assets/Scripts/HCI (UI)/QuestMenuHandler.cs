using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenuHandler : MonoBehaviour
{
    public Text QuestName;
    public Text QuestStatus;
    public Text QuestDesc;
    public Text QuestObjName;
    public Text QuestObj;
    public Image QuestType;

    public int PotionReward; //remove upon trader workings
    public int GoldReward;

    public Image QuestGiver;
    public Image QuestGiverLocation;
    

    void QuestButton()
    {

        #region QuestBoard
        //SET QuestName
        //SET QuestStatus
        //SET QuestDesc
        //SET QuestObjName
        //SET QuestObj

        //SET Potionrewards
        //SET gold rewards
        #endregion

        #region ArtWork
        //SET quest giver sprite
        //SET quest giver location art
        #endregion

        #region Quest Buttons (selector)
        //SET QuestName

        //IF(QuestStatus == "Complete")
        //Icon == Yellow Boarder
        //IF (QuestStatus == "InProgress")
        //Icon == Orange Boarder
        #endregion
    }
}
