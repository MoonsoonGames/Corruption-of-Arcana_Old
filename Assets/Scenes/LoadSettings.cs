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

    /*
    public bool enemyKilled = false;
    public bool bossKilled = false;

    */
    public bool fightingBoss = false;

    E_Levels lastLevel;
    public Vector3 playerPosInThoth;

    public Vector3 checkPointPos;
    Scene checkPointScene;

    public bool died;

    public Object[] enemies = new Object[3];

    public int checkPointPotionCount;
    public int potionCount;

    bool main = false;

    public Dictionary<string, bool> enemiesKilled = new Dictionary<string, bool>();
    public Dictionary<string, bool> checkpointEnemies = new Dictionary<string, bool>();

    public string currentFight;

    private void Awake()
    {
        LoadSettings[] loadSettings = GameObject.FindObjectsOfType<LoadSettings>();

        Debug.Log(loadSettings.Length);

        if (loadSettings.Length > 1)
        {
            Debug.Log("destroying");
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
            Debug.Log("destroying");
            return true;
        }
    }

    public Vector3 RequestPosition(PlayerController controller)
    {
        Vector3 targetPos;

        if (died)
        {
            died = false;

            targetPos = checkPointPos;

            targetPos.x = checkPointPos.x;
            targetPos.y = checkPointPos.y;
            targetPos.z = checkPointPos.z;

            Debug.Log("Loading respawn position | " + checkPointPos + " || " + targetPos);
        }
        else
        {
            targetPos = playerPosInThoth;

            targetPos.x = playerPosInThoth.x;
            targetPos.y = playerPosInThoth.y;
            targetPos.z = playerPosInThoth.z;

            Debug.Log("Loading spawn position | " + playerPosInThoth + " || " + targetPos);

            controller.transform.position = targetPos;
            Debug.Log(controller.transform.position);
        }

        return targetPos;
    }

    public E_Levels GetLastLevel()
    {
        return lastLevel;
    }

    public void SetLastLevel(E_Levels newLevel)
    {
        lastLevel = newLevel;
    }

    public Scene GetCheckpointScene()
    {
        return checkPointScene;
    }

    public void Checkpoint(Scene newCheckPoint)
    {
        checkpointEnemies = enemiesKilled;

        SetCheckPointLevel(newCheckPoint);
    }

    public void SetCheckPointLevel(Scene newCheckPoint)
    {
        checkPointScene = newCheckPoint;
    }
}
