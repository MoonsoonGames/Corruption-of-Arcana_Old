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
    public Vector3 playerPosInClearing;

    public Vector3 checkPointPos;
    public Scene checkPointScene;
    public string checkPointString;

    public bool died;

    public Object[] enemies = new Object[3];

    public int checkPointPotionCount;
    public int potionCount;

    bool main = false;

    public Dictionary<string, bool> enemiesKilled = new Dictionary<string, bool>();
    public Dictionary<string, bool> checkpointEnemies = new Dictionary<string, bool>();

    public List<string> enemiesString;
    public List<string> killedString;

    public string currentFight;

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
            return true;
        }
    }

    public Vector3 RequestPosition(PlayerController controller, string scene)
    {
        Vector3 targetPos;

        if (died)
        {
            died = false;

            targetPos = checkPointPos;

            targetPos.x = checkPointPos.x;
            targetPos.y = checkPointPos.y;
            targetPos.z = checkPointPos.z;

            //Debug.Log("Loading respawn position | " + checkPointPos + " || " + targetPos);
        }
        else
        {
            targetPos = new Vector3();
            //Debug.Log(scene);
            lastLevelString = scene;
            if (scene == E_Levels.Thoth.ToString())
            {
                targetPos = playerPosInThoth;

                targetPos.x = playerPosInThoth.x;
                targetPos.y = playerPosInThoth.y;
                targetPos.z = playerPosInThoth.z;
            }
            else if (scene == E_Levels.Clearing.ToString())
            {
                targetPos = playerPosInClearing;

                targetPos.x = playerPosInClearing.x;
                targetPos.y = playerPosInClearing.y;
                targetPos.z = playerPosInClearing.z;
            }

            //Debug.Log("Loading spawn position | " + playerPosInThoth + " || " + targetPos);

            controller.transform.position = targetPos;
            //Debug.Log(controller.transform.position);
        }

        return targetPos;
    }

    public void Checkpoint(Scene newCheckPoint)
    {
        checkpointEnemies = enemiesKilled;

        checkPointScene = newCheckPoint;
        checkPointString = checkPointScene.name;
    }

    private void Update()
    {
        enemiesString.Clear();
        killedString.Clear();
        foreach (var item in enemiesKilled)
        {
            if (item.Value)
            {
                killedString.Add(item.Key);
            }
            else
            {
                enemiesString.Add(item.Key);
            }
        }
    }
}
