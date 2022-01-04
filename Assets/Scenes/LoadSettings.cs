using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSettings : MonoBehaviour
{
    public int health = 1;

    #region Tutorial Dialogue

    public bool dialogueComplete = false;
    public bool prologueComplete = false;

    #endregion 

    public bool fightingBoss = false;

    public E_Levels lastLevel;
    public string lastLevelString;
    public Vector3 playerPosInThoth;
    public Quaternion playerRotInThoth;
    public Vector3 playerPosInClearing;
    public Quaternion playerRotInClearing;

    public Vector3 checkPointPos;
    public Quaternion checkPointRot;
    public Scene checkPointScene;
    public string checkPointString;

    public bool died;

    public Object[] enemies = new Object[3];

    public int checkPointPotionCount;
    public int potionCount;

    bool main = false;
    /*
    public Dictionary<string, bool> enemiesKilled = new Dictionary<string, bool>();
    public Dictionary<string, bool> checkpointEnemies = new Dictionary<string, bool>();
    */
    //public List<string> enemiesAlive;
    public List<string> enemiesKilled = new List<string>();
    public List<string> checkpointEnemies = new List<string>();

    public int currentGold;
    public int checkPointGold;

    public string currentFight;
    public Vector2 goldReward;
    public float potionReward;
    public string itemReward;

    private void Awake()
    {
        LoadSettings[] loadSettings = GameObject.FindObjectsOfType<LoadSettings>();

        //Debug.Log(loadSettings.Length);

        if (loadSettings.Length > 1)
        {
            //Debug.Log("destroying");
            Destroy(this); //There is already one in the scene, delete this one
        }
        else
        {
            main = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public bool CheckMain()
    {
        if (main)
        {
            return true;
        }
        else
        {
            //Debug.Log("destroying");
            return false;
        }
    }

    public Vector3 RequestPosition(string scene)
    {
        Vector3 targetPos;

        if (died)
        {
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

            Debug.Log("Loading spawn rotation | " + playerRotInThoth.eulerAngles + " || " + targetRot.eulerAngles);

        }

        return targetRot;
    }

    public void ResetEnemies()
    {
        Debug.Log("Reset Enemies");
        enemiesKilled.Clear();

        foreach (var item in checkpointEnemies)
        {
            enemiesKilled.Add(item);
        }
    }

    public void Checkpoint(Scene newCheckPoint)
    {
        Debug.Log("Checkpoint");
        checkpointEnemies.Clear();

        foreach (var item in enemiesKilled)
        {
            checkpointEnemies.Add(item);
        }

        checkPointScene = newCheckPoint;
        checkPointString = checkPointScene.name;
    }
}
