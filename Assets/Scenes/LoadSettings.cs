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

    public Vector3 checkpointPos;
    public Quaternion checkPointRot;
    public Scene checkPointScene;
    public string checkPointString;

    public bool died;
    public bool checkPoint = false;

    #endregion

    #region Stats

    public bool fightingBoss = false;
    public Object[] enemies = new Object[3];
    public Sprite background;

    public List<string> enemiesKilled = new List<string>();
    public List<string> bossesKilled = new List<string>();

    public int maxHealingPotionCount;
    public int healingPotionCount;
    public int checkpointArcanaPotionCount;
    public int arcanaPotionCount;

    public int currentGold;

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
            LoadCheckpointData();

            targetPos = checkpointPos;

            targetPos.x = checkpointPos.x;
            targetPos.y = checkpointPos.y;
            targetPos.z = checkpointPos.z;

            //Debug.Log("Loading respawn position | " + checkPointPos + " || " + targetPos);
        }
        else
        {
            targetPos = new Vector3();
            
            SetScene(scene);

            //Debug.Log(scene + " and " + lastLevelString);

            if (lastLevel.ToString() == E_Levels.Thoth.ToString())
            {
                targetPos = playerPosInThoth;

                targetPos.x = playerPosInThoth.x;
                targetPos.y = playerPosInThoth.y;
                targetPos.z = playerPosInThoth.z;
            }
            else if (lastLevel.ToString() == E_Levels.EastForestClearing.ToString())
            {
                Debug.Log(scene + " and " + lastLevel.ToString());
                targetPos = playerPosInClearing;

                targetPos.x = playerPosInClearing.x;
                targetPos.y = playerPosInClearing.y;
                targetPos.z = playerPosInClearing.z;
            }
            else if (lastLevel.ToString() == E_Levels.EasternCave.ToString())
            {
                Debug.Log(scene + " and " + lastLevel.ToString());
                targetPos = playerPosInCave;

                targetPos.x = playerPosInCave.x;
                targetPos.y = playerPosInCave.y;
                targetPos.z = playerPosInCave.z;
            }
            else if (lastLevel.ToString() == E_Levels.Tiertarock.ToString())
            {
                Debug.Log(scene + " and " + lastLevel.ToString());
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

            SetScene(scene);

            //Debug.Log(scene + " and " + lastLevelString);

            if (lastLevel.ToString() == E_Levels.Thoth.ToString())
            {
                targetRot = playerRotInThoth;

                targetRot.x = playerRotInThoth.x;
                targetRot.y = playerRotInThoth.y;
                targetRot.z = playerRotInThoth.z;
                targetRot.w = playerRotInThoth.w;
            }
            else if (lastLevel.ToString() == E_Levels.EastForestClearing.ToString())
            {
                Debug.Log(scene + " and " + lastLevel.ToString());
                targetRot = playerRotInClearing;

                targetRot.x = playerRotInClearing.x;
                targetRot.y = playerRotInClearing.y;
                targetRot.z = playerRotInClearing.z;
                targetRot.w = playerRotInClearing.w;
            }
            else if (lastLevel.ToString() == E_Levels.EasternCave.ToString())
            {
                Debug.Log(scene + " and " + lastLevel.ToString());
                targetRot = playerRotInCave;

                targetRot.x = playerRotInCave.x;
                targetRot.y = playerRotInCave.y;
                targetRot.z = playerRotInCave.z;
            }
            else if (lastLevel.ToString() == E_Levels.Tiertarock.ToString())
            {
                Debug.Log(scene + " and " + lastLevel.ToString());
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

    #region Last Level

    public void SetScene(string scene)
    {
        //https://answers.unity.com/questions/52162/converting-a-string-to-an-enum.html
        lastLevel = (E_Levels)System.Enum.Parse(typeof(E_Levels), scene);
    }

    #endregion

    #region Checkpoints

    public void ResetEnemies()
    {
        Debug.Log("Reset Enemies");
        enemiesKilled.Clear();

        foreach (var item in bossesKilled)
        {
            enemiesKilled.Add(item);
        }
    }

    public void SetCheckpoint()
    {
        checkPoint = true;
    }

    public void LoadCheckpointData()
    {
        #region Reset last position and quest data

        questSaver.LoadQuestData();

        //Resets last position in these levels
        if (lastLevel.ToString() == E_Levels.Thoth.ToString())
        {
            playerPosInThoth = new Vector3(-358.679993f, 38.8400002f, 288.880005f);
        }
        else if (lastLevel.ToString() == E_Levels.EastForestClearing.ToString())
        {
            playerPosInClearing = new Vector3(37f, 7.61000013f, 294.160004f);
        }
        else if (lastLevel.ToString() == E_Levels.EasternCave.ToString())
        {
            Debug.Log("Cave was last scene");
            playerPosInCave = new Vector3(37f, 45.2999992f, 260.899994f);
        }
        else
        {
            Debug.Log("Error: " + lastLevel.ToString() + " || " + E_Levels.EasternCave.ToString());
        }

        currentNodeID = checkpointNodeID;

        health = maxHealth;
        healingPotionCount = maxHealingPotionCount;

        #endregion

        ResetEnemies();
    }

    public void SaveCheckpoint(Scene newCheckPoint, PlayerController controller)
    {
        Debug.Log("Checkpoint");

        if (newCheckPoint != null)
        {
            checkPointScene = newCheckPoint;
            checkPointString = checkPointScene.name;
        }

        if (controller != null)
        {
            Debug.Log(SceneManager.GetActiveScene());

            checkpointPos = controller.transform.position;
            checkPointScene = SceneManager.GetActiveScene();
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

    #region Deckbuilding

    public List<CardParent> basicArcana;
    public List<CardParent> majourArcana;
    public List<CardParent> corruptedArcana;

    public int goldPerCard = 20;

    public void ResetCards(bool cache)
    {
        if (cache)
        {
            Debug.Log("Cache cards");
            currentGold += DetermineGoldFromCards();
        }
        else
        {
            Debug.Log("Reset cards");
        }

        majourArcana.Clear();
    }

    public int DetermineGoldFromCards()
    {
        return majourArcana.Count * goldPerCard;
    }

    #endregion

    #region Upgrades

    public int health = 100;
    public int maxHealth = 100;

    public void IncreaseMaxHealth(int increase)
    {
        maxHealth += increase;
    }

    public int GetHeathIncreaseCost()
    {
        return maxHealth;
    }

    #endregion
}