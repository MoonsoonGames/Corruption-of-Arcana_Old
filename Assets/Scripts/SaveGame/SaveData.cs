using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    #region Levels

    public E_Levels lastLevel;
    public float[] playerPosInThoth = new float[3];
    public float[] playerRotInThoth = new float[4];
    public float[] playerPosInClearing = new float[3];
    public float[] playerRotInClearing = new float[4];
    public float[] playerPosInCave = new float[3];
    public float[] playerRotInCave = new float[4];
    public float[] playerPosInTiertarock = new float[3];
    public float[] playerRotInTiertarock = new float[4];

    #endregion

    #region Checkpoints

    public float[] checkpointPos = new float[3];
    public float[] checkPointRot = new float[4];
    public Scene checkPointScene;
    public string checkPointString;

    public bool died;
    public bool checkPoint = false;

    #endregion

    #region Stats

    public List<string> enemiesKilled = new List<string>();
    public List<string> bossesKilled = new List<string>();

    public int currentGold;

    public List<string> revealedEntries = new List<string>();

    #endregion

    #region Travelling

    public string currentNodeID;
    public string checkpointNodeID;

    #endregion

    #region Quests

    public QuestData[] quests;
    public ObjectiveData[] objectives;

    #endregion

    #region Deckbuilding

    public string equippedWeapon;
    public List<string> earnedWeapons = new List<string>();
    public string rewardWeapon;

    public List<string> majourArcana = new List<string>();
    public List<string> corruptedArcana = new List<string>();

    #endregion

    #region Upgrades

    public int health = 100;
    public int maxHealth = 100;

    public int maxHealingPotionCount;
    public int healingPotionCount;
    public int checkpointArcanaPotionCount;
    public int arcanaPotionCount;

    #endregion

    public SaveData(LoadSettings loadSettings)
    {
        if (loadSettings != null)
        {
            #region Enemies

            enemiesKilled = loadSettings.enemiesKilled;
            bossesKilled = loadSettings.bossesKilled;

            #endregion

            #region Scenes

            lastLevel = loadSettings.lastLevel;
            checkPointScene = loadSettings.checkPointScene;
            checkPointString = loadSettings.checkPointString;

            playerPosInThoth[0] = loadSettings.playerPosInThoth.x;
            playerPosInThoth[1] = loadSettings.playerPosInThoth.y;
            playerPosInThoth[2] = loadSettings.playerPosInThoth.z;
            playerRotInThoth[0] = loadSettings.playerRotInThoth.x;
            playerRotInThoth[1] = loadSettings.playerRotInThoth.y;
            playerRotInThoth[2] = loadSettings.playerRotInThoth.z;
            playerRotInThoth[3] = loadSettings.playerRotInThoth.w;

            playerPosInClearing[0] = loadSettings.playerPosInClearing.x;
            playerPosInClearing[1] = loadSettings.playerPosInClearing.y;
            playerPosInClearing[2] = loadSettings.playerPosInClearing.z;
            playerRotInClearing[0] = loadSettings.playerRotInClearing.x;
            playerRotInClearing[1] = loadSettings.playerRotInClearing.y;
            playerRotInClearing[2] = loadSettings.playerRotInClearing.z;
            playerRotInClearing[3] = loadSettings.playerRotInClearing.w;

            playerPosInCave[0] = loadSettings.playerPosInCave.x;
            playerPosInCave[1] = loadSettings.playerPosInCave.y;
            playerPosInCave[2] = loadSettings.playerPosInCave.z;
            playerRotInCave[0] = loadSettings.playerRotInCave.x;
            playerRotInCave[1] = loadSettings.playerRotInCave.y;
            playerRotInCave[2] = loadSettings.playerRotInCave.z;
            playerRotInCave[3] = loadSettings.playerRotInCave.w;

            playerPosInTiertarock[0] = loadSettings.playerPosInTiertarock.x;
            playerPosInTiertarock[1] = loadSettings.playerPosInTiertarock.y;
            playerPosInTiertarock[2] = loadSettings.playerPosInTiertarock.z;
            playerRotInTiertarock[0] = loadSettings.playerRotInTiertarock.x;
            playerRotInTiertarock[1] = loadSettings.playerRotInTiertarock.y;
            playerRotInTiertarock[2] = loadSettings.playerRotInTiertarock.z;
            playerRotInTiertarock[3] = loadSettings.playerRotInTiertarock.w;

            checkpointPos[0] = loadSettings.checkpointPos.x;
            checkpointPos[1] = loadSettings.checkpointPos.y;
            checkpointPos[2] = loadSettings.checkpointPos.z;
            checkPointRot[0] = loadSettings.checkPointRot.x;
            checkPointRot[1] = loadSettings.checkPointRot.y;
            checkPointRot[2] = loadSettings.checkPointRot.z;
            checkPointRot[3] = loadSettings.checkPointRot.w;

            #endregion

            #region Guidebook

            revealedEntries.Clear();
            revealedEntries = loadSettings.revealedEntries;

            #endregion

            #region Travel

            currentNodeID = loadSettings.currentNodeID;
            checkpointNodeID = loadSettings.checkpointNodeID;

            #endregion

            #region Quests

            if (quests == null)
            {
                quests = new QuestData[Resources.FindObjectsOfTypeAll<Quest>().Length];

                for (int i = 0; i < loadSettings.questSaver.quests.Length; i++)
                {
                    Quest quest = loadSettings.questSaver.quests[i];

                    if (quest != null)
                    {
                        Debug.Log(i + " " + quest.name);
                        quests[i].name = quest.name;
                        quests[i].isActive = quest.isActive;
                        quests[i].isComplete = quest.isComplete;
                        quests[i].isRevealled = quest.isRevealled;
                        quests[i].currentObjective = quest.name;
                    }
                }
            }

            if (objectives == null)
            {
                objectives = new ObjectiveData[loadSettings.questSaver.objectives.Length];
                for (int i = 0; i < loadSettings.questSaver.objectives.Length; i++)
                {
                    objectives[i].name = loadSettings.questSaver.objectives[i].name;
                    objectives[i].canComplete = loadSettings.questSaver.objectives[i].canComplete;
                    objectives[i].completed = loadSettings.questSaver.objectives[i].completed;
                    objectives[i].currentAmount = loadSettings.questSaver.objectives[i].currentAmount;
                }
            }

            foreach (var quest in quests)
            {
                Quest loadQuest = loadSettings.questSaver.GetQuestFromData(quest.name);

                quest.isActive = loadQuest.isActive;
                quest.isComplete = loadQuest.isComplete;
                quest.isRevealled = loadQuest.isRevealled;
                quest.currentObjective = loadQuest.currentObjective.name;
            }

            foreach (var objective in objectives)
            {
                QuestObjective loadQuest = loadSettings.questSaver.GetObjectiveFromData(objective.name);

                objective.canComplete = loadQuest.canComplete;
                objective.completed = loadQuest.completed;
                objective.currentAmount = loadQuest.currentAmount;
            }

            #endregion

            #region Upgrades

            health = loadSettings.health;
            maxHealth = loadSettings.maxHealth;

            maxHealingPotionCount = loadSettings.maxHealingPotionCount;
            healingPotionCount = loadSettings.healingPotionCount;
            checkpointArcanaPotionCount = loadSettings.checkpointArcanaPotionCount;
            arcanaPotionCount = loadSettings.arcanaPotionCount;

            #endregion

            #region Decks

            equippedWeapon = loadSettings.equippedWeapon.weaponName;
            foreach (var item in loadSettings.earnedWeapons)
            {
                earnedWeapons.Add(item.weaponName);
            }

            foreach (var item in loadSettings.majourArcana)
            {
                earnedWeapons.Add(item.cardName);
            }
            foreach (var item in loadSettings.corruptedArcana)
            {
                earnedWeapons.Add(item.cardName);
            }

            currentGold = loadSettings.currentGold;

            #endregion
        }
        else
        {
            Debug.LogError("No load settings");
        }
    }
}

[System.Serializable]
public class QuestData
{
    public string name;
    public bool isActive;
    public bool isComplete;
    public bool isRevealled;
    public string currentObjective;
}

[System.Serializable]
public class ObjectiveData
{
    public string name;
    public bool canComplete;
    public bool completed;
    public int currentAmount;
}