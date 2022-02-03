using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewEvent", menuName = "Navigation/Events", order = 0)]
public class NavigationEvents : ScriptableObject
{
    LoadSettings loadSettings;
    SceneLoader sceneLoader;

    public string eventName;
    public bool boon;
    public bool bane;

    E_Levels navScene;
    public E_Levels loadScene;
    public Vector2Int goldReward;
    public float potionReward;

    /*
    public int boonPrevention;
    public int banePevention;
    */

    public Object[] enemy1, enemy2, enemy3;
    Object[] enemies = new Object[3];

    public void Setup(SceneLoader newSceneLoader, E_Levels newNavScene)
    {
        loadSettings = GameObject.FindObjectOfType<LoadSettings>();
        navScene = newNavScene;
        sceneLoader = newSceneLoader;

        enemies[0] = enemy1[Random.Range(0, enemy1.Length)];
        enemies[1] = enemy2[Random.Range(0, enemy2.Length)];
        enemies[2] = enemy3[Random.Range(0, enemy3.Length)];
    }

    public void StartEvent()
    {
        bool fight = false;

        foreach (var item in enemies)
        {
            if (item != null)
            {
                fight = true;
            }
        }

        if (fight)
        {
            LoadCombat();
        }
        else
        {
            //give player rewards now
        }
    }

    void LoadCombat()
    {
        if (loadSettings != null)
        {
            loadSettings.fightingBoss = false;
            loadSettings.currentFight = "Random Event Fight";

            loadSettings.enemies[0] = enemies[0];
            loadSettings.enemies[1] = enemies[1];
            loadSettings.enemies[2] = enemies[2];

            loadSettings.goldReward = goldReward;
            loadSettings.potionReward = potionReward;
            //loadSettings.itemReward = itemReward;

            loadSettings.lastLevel = navScene;
            loadSettings.lastLevelString = navScene.ToString();

            if (sceneLoader != null)
                sceneLoader.LoadSpecifiedScene(loadScene.ToString(), LoadSceneMode.Single, null);
        }
    }
}
