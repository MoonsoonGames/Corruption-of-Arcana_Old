using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    public float health = 1.2f;

    public bool dialogueComplete = false;

    public bool enemyKilled = false;
    public bool bossKilled = false;

    public bool fightingBoss = false;

    public Vector3 playerPos;
    public Vector3 checkPointPos;

    public bool died;

    public Object[] enemies = new Object[3];

    public int checkPointPotionCount;
    public int potionCount;

    bool main = false;

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
            targetPos = playerPos;

            targetPos.x = playerPos.x;
            targetPos.y = playerPos.y;
            targetPos.z = playerPos.z;

            Debug.Log("Loading spawn position | " + playerPos + " || " + targetPos);

            controller.transform.position = targetPos;
            Debug.Log(controller.transform.position);
        }

        return targetPos;
    }
}
