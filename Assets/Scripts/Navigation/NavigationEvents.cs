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

    Sprite background;

    public Quest startQuest;
    public QuestObjective[] completeObjectives;
    public string questGiverName;
    public Sprite questGiverSprite;

    public void Setup(SceneLoader newSceneLoader, E_Levels newNavScene, Sprite backgroundImage)
    {
        loadSettings = LoadSettings.instance;
        navScene = newNavScene;
        sceneLoader = newSceneLoader;

        background = backgroundImage;

        enemies[0] = enemy1[Random.Range(0, enemy1.Length)];
        enemies[1] = enemy2[Random.Range(0, enemy2.Length)];
        enemies[2] = enemy3[Random.Range(0, enemy3.Length)];
    }

    public void StartEvent()
    {
        if (startQuest != null)
        {
            startQuest.AcceptQuest(questGiverName);

            if (loadSettings.quests.Contains(startQuest) == false)
            {
                loadSettings.quests.Add(startQuest);
            }
        }

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
            GiveRewards();

            foreach (var item in completeObjectives)
            {
                if (item.canComplete)
                {
                    item.CompleteGoal();
                }
            }
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

            loadSettings.background = background;

            loadSettings.goldReward = goldReward;
            loadSettings.potionReward = potionReward;
            //loadSettings.itemReward = itemReward;

            foreach (var item in completeObjectives)
            {
                if (item.canComplete)
                {
                    loadSettings.currentFightObjectives.Add(item);
                }
            }

            //loadSettings.lastLevel = navScene;

            if (sceneLoader != null)
                sceneLoader.LoadSpecifiedScene(loadScene.ToString(), LoadSceneMode.Single, null);
        }
    }

    #region Rewards

    void GiveRewards()
    {
        Debug.Log("Give rewards");
        if (loadSettings != null && loadSettings.currentFight != null)
        {
            loadSettings.currentGold += (int)Random.Range(goldReward.x, goldReward.y);
            loadSettings.healingPotionCount = DeterminePotions(loadSettings.healingPotionCount);
        }
    }

    int DeterminePotions(float potions)
    {
        int potionsReward = (int)potions;
        float chance = potions % 1f;

        int test = potionsReward;

        if (RandomBoolWeighting(chance))
            potionsReward++;

        //Debug.Log(test + " | " + chance + " | " + potionsReward);

        return Mathf.Clamp(potionsReward, 0, 5);
    }

    //From Gam140 Godsent by Andrew Scott
    private bool RandomBoolWeighting(float weighting)
    {
        if (Random.value >= weighting)
        {
            return true;
        }
        return false;
    }

    #endregion
}
