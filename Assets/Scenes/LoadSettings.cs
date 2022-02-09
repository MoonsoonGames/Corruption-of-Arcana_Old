using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSettings : MonoBehaviour
{
    #region Setup

    #region Variables

    #region Dialogue

    public Object dialogueFlowChart;
    public bool loadSceneMultiple = false;

    #endregion 

    #region Exploration

    #region Levels

    public E_Levels lastLevel;
    public string lastLevelString;
    public Vector3 playerPosInThoth;
    public Quaternion playerRotInThoth;
    public Vector3 playerPosInClearing;
    public Quaternion playerRotInClearing;
    public Vector3 playerPosInCave;
    public Quaternion playerRotInCave;
    public Vector3 playerPosInTiertarock;
    public Quaternion playerRotInTiertarock;

    #endregion

    #region Checkpoints

    public Vector3 checkPointPos;
    public Quaternion checkPointRot;
    public Scene checkPointScene;
    public string checkPointString;

    public bool died;
    public bool checkPoint = false;

    #endregion

    #region Stats

    public bool fightingBoss = false;
    public Object[] enemies = new Object[3];
    public List<string> enemiesKilled = new List<string>();
    public List<string> checkpointEnemies = new List<string>();

    public int health = 1;
    public int checkPointPotionCount;
    public int potionCount;

    public int currentGold;
    public int checkPointGold;

    public string currentFight;
    public Vector2 goldReward;
    public float potionReward;
    public string itemReward;

    #endregion

    #endregion

    #endregion

    private void Awake()
    {
        Singleton();

        questSaver = GetComponent<SaveLoadQuestData>();

        DontDestroyOnLoad(this);
    }

    #endregion

    #region Singleton

    public static LoadSettings instance = null;

    void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Player

    #region Player Position and Rotation

    public Vector3 RequestPosition(string scene)
    {
        Vector3 targetPos;

        if (died)
        {
            #region Reset last position and quest data

            questSaver.LoadQuestData();

            if (lastLevelString == E_Levels.Thoth.ToString())
            {
                playerPosInThoth = new Vector3(-358.679993f, 38.8400002f, 288.880005f);
            }
            else if (lastLevelString == E_Levels.EastForestClearing.ToString())
            {
                playerPosInClearing = new Vector3(37f, 7.61000013f, 294.160004f);
            }

            currentNodeID = checkpointNodeID;

            #endregion

            ResetEnemies();
            targetPos = checkPointPos;

            targetPos.x = checkPointPos.x;
            targetPos.y = checkPointPos.y;
            targetPos.z = checkPointPos.z;

            //Debug.Log("Loading respawn position | " + checkPointPos + " || " + targetPos);
        }
        else
        {
            targetPos = new Vector3();
            
            lastLevelString = scene;

            //Debug.Log(scene + " and " + lastLevelString);

            if (lastLevelString == E_Levels.Thoth.ToString())
            {
                targetPos = playerPosInThoth;

                targetPos.x = playerPosInThoth.x;
                targetPos.y = playerPosInThoth.y;
                targetPos.z = playerPosInThoth.z;
            }
            else if (lastLevelString == E_Levels.EastForestClearing.ToString())
            {
                Debug.Log(scene + " and " + lastLevelString);
                targetPos = playerPosInClearing;

                targetPos.x = playerPosInClearing.x;
                targetPos.y = playerPosInClearing.y;
                targetPos.z = playerPosInClearing.z;
            }
            else if (lastLevelString == E_Levels.Cave.ToString())
            {
                Debug.Log(scene + " and " + lastLevelString);
                targetPos = playerPosInCave;

                targetPos.x = playerPosInCave.x;
                targetPos.y = playerPosInCave.y;
                targetPos.z = playerPosInCave.z;
            }
            else if (lastLevelString == E_Levels.Tiertarock.ToString())
            {
                Debug.Log(scene + " and " + lastLevelString);
                targetPos = playerPosInTiertarock;

                targetPos.x = playerPosInTiertarock.x;
                targetPos.y = playerPosInTiertarock.y;
                targetPos.z = playerPosInTiertarock.z;
            }

            //Debug.Log("Loading spawn position | " + playerPosInThoth + " || " + targetPos);

        }

        return targetPos;
    }

    public Quaternion RequestRotation(string scene)
    {
        Quaternion targetRot;

        if (died)
        {
            ResetEnemies();
            targetRot = checkPointRot;

            targetRot.x = checkPointRot.x;
            targetRot.y = checkPointRot.y;
            targetRot.z = checkPointRot.z;
            targetRot.w = checkPointRot.w;

            //Debug.Log("Loading respawn position | " + checkPointPos + " || " + targetPos);
        }
        else
        {
            targetRot = new Quaternion();

            lastLevelString = scene;

            //Debug.Log(scene + " and " + lastLevelString);

            if (lastLevelString == E_Levels.Thoth.ToString())
            {
                targetRot = playerRotInThoth;

                targetRot.x = playerRotInThoth.x;
                targetRot.y = playerRotInThoth.y;
                targetRot.z = playerRotInThoth.z;
                targetRot.w = playerRotInThoth.w;
            }
            else if (lastLevelString == E_Levels.EastForestClearing.ToString())
            {
                Debug.Log(scene + " and " + lastLevelString);
                targetRot = playerRotInClearing;

                targetRot.x = playerRotInClearing.x;
                targetRot.y = playerRotInClearing.y;
                targetRot.z = playerRotInClearing.z;
                targetRot.w = playerRotInClearing.w;
            }
            else if (lastLevelString == E_Levels.Cave.ToString())
            {
                Debug.Log(scene + " and " + lastLevelString);
                targetRot = playerRotInCave;

                targetRot.x = playerRotInCave.x;
                targetRot.y = playerRotInCave.y;
                targetRot.z = playerRotInCave.z;
            }
            else if (lastLevelString == E_Levels.Tiertarock.ToString())
            {
                Debug.Log(scene + " and " + lastLevelString);
                targetRot = playerRotInTiertarock;

                targetRot.x = playerRotInTiertarock.x;
                targetRot.y = playerRotInTiertarock.y;
                targetRot.z = playerRotInTiertarock.z;
            }

            //Debug.Log("Loading spawn rotation | " + playerRotInThoth.eulerAngles + " || " + targetRot.eulerAngles);
        }

        return targetRot;
    }

    #endregion

    #region Inputs

    public void SetPlayerInput(bool enabled)
    {
        PlayerController controller = GameObject.FindObjectOfType<PlayerController>();

        if (controller != null)
        {
            controller.canMove = enabled;
        }
    }

    #endregion

    #endregion

    #region Checkpoints

    public void ResetEnemies()
    {
        Debug.Log("Reset Enemies");
        enemiesKilled.Clear();

        foreach (var item in checkpointEnemies)
        {
            enemiesKilled.Add(item);
        }
    }

    public void SetCheckpoint()
    {
        checkPoint = true;
    }

    public void SaveCheckpoint(Scene newCheckPoint)
    {
        Debug.Log("Checkpoint");
        checkpointEnemies.Clear();

        foreach (var item in enemiesKilled)
        {
            checkpointEnemies.Add(item);
        }

        if (newCheckPoint != null)
        {
            checkPointScene = newCheckPoint;
            checkPointString = checkPointScene.name;
        }

        checkpointNodeID = currentNodeID;

        questSaver.SaveQuestData();

        checkPoint = false;
    }

    #endregion

    #region Guidebook

    public List<string> revealedEntries = new List<string>();

    public void AddToGuidebook(string name)
    {
        if (revealedEntries.Contains(name))
        {
            //
        }
        else
        {
            revealedEntries.Add(name);
            //sort
        }
    }

    static int SortByInt(Object e1, Object e2)
    {
        GameObject gObject1 = e1 as GameObject;
        GameObject gObject2 = e2 as GameObject;

        Enemy stats1 = gObject1.GetComponent<Enemy>();
        Enemy stats2 = gObject2.GetComponent<Enemy>();

        return stats1.guidebookOrder.CompareTo(stats2.guidebookOrder);
    }

    public bool CheckExposed(string name)
    {
        return revealedEntries.Contains(name);
    }

    #endregion

    #region Travelling

    public string currentNodeID;
    public string checkpointNodeID;

    public void TravelStart(NavigationNode newNode)
    {
        currentNodeID = newNode.name;
    }

    #endregion

    #region Quests

    public List<Quest> quests;
    public QuestObjective currentFightObjective;
    SaveLoadQuestData questSaver;

    #endregion
}